namespace Winl.AzureDevBox.ConsoleDemo.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Winl.AzureDevBox.AzurePlatform.EventHubs;

    internal sealed class EventHubDemo : DemoBase
    {
        private const string SecretPrefix = "AzureDevBox:EventHubDemo:";

        private readonly IConfiguration config;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubDemo"/> class.
        /// </summary>
        public EventHubDemo()
            : base(enableSyncDemo: false, enableAsyncDemo: true)
        {
            IConfigurationBuilder configBuilder =
                new ConfigurationBuilder().AddUserSecrets<EventHubDemo>();

            this.config = configBuilder.Build();
        }

        #endregion

        private string EventHubConnectionString
            => this.config[$"{SecretPrefix}EventHubConnectionString"];

        private string EventHubEntityPath
            => this.config[$"{SecretPrefix}EventHubEntityPath"];

        private string EventHubConsumerGroup
            => this.config[$"{SecretPrefix}EventHubConsumerGroup"];

        private string StorageAccountConnectionString
            => this.config[$"{SecretPrefix}StorageAccountConnectionString"];

        private string StorageAccountContainer
            => this.config[$"{SecretPrefix}StorageAccountContainer"];

        protected override async Task InternalRunAsync()
        {
            IEventHubClient eventHubClient =
                new EventHubClient(
                    this.EventHubConnectionString,
                    this.EventHubEntityPath);

            this.PrintObjectInfo(eventHubClient);

            IEventHubRuntimeInformation runtimeInformation =
                await eventHubClient.GetEventHubRuntimeInformationAsync()
                    .ConfigureAwait(false);

            this.PrintObjectInfo(runtimeInformation);

            IEnumerable<IEventHubPartitionRuntimeInformation> partionInfos =
                runtimeInformation.PartitionIds
                    .Select(
                        pid => eventHubClient
                            .GetPartitionRuntimeInformationAsync(pid)
                            .ConfigureAwait(false).GetAwaiter().GetResult())
                    .ToArray();

            this.PrintObjectInfo(partionInfos);

            int batchCount = 500;
            int batchSize = 100;
            for (int batchIdx = 0; batchIdx < batchCount; ++batchIdx)
            {
                await eventHubClient
                    .BatchSendAsync(
                        new int[batchSize].Select(
                            (v, i) => $"Data {batchIdx}-{i} at {DateTime.UtcNow:o} dummy: {new string('$', 256)}"))
                    .ConfigureAwait(false);
            }
            Console.WriteLine($"Total sent: {batchCount * batchSize}");

            IEventProcessor eventProcessor =
                new EventProcessor(
                   this.EventHubConsumerGroup,
                   this.StorageAccountConnectionString,
                   this.StorageAccountContainer)
                   {
                       Options = new EventProcessorOptions
                       {
                           MaxBatchSize = 50,
                           PrefetchCount = 500
                       }
                   };

            int totalReceived = 0;

            Dictionary<string, List<IEventData>> bufferDic =
                new Dictionary<string, List<IEventData>>();

            Dictionary<string, DateTimeOffset> lastDumpTimeDic =
                new Dictionary<string, DateTimeOffset>();

            Dictionary<string, IEventHubPartitionContext> partitionContextDic =
                new Dictionary<string, IEventHubPartitionContext>();

            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();
            TaskFactory taskFactory =
                new TaskFactory(cancellationTokenSource.Token);
            List<Task> tasksList = new List<Task>();

            DateTimeOffset initialTime =
                DateTimeOffset.FromUnixTimeSeconds(
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            foreach (var partitionInfo in partionInfos)
            {
                string partitionId = partitionInfo.PartitionId;
                bufferDic[partitionId] = new List<IEventData>();
                lastDumpTimeDic[partitionId] =
                    initialTime.AddSeconds(
                        int.Parse(partitionId) % partionInfos.Count());

                Console.WriteLine($"last dump time for partition {partitionId}: {lastDumpTimeDic[partitionId]}");

                Task task = taskFactory.StartNew(
                    async () =>
                    {
                        while (true)
                        {
                            DateTimeOffset checkTime =
                                DateTimeOffset.FromUnixTimeSeconds(
                                    DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                            List<IEventData> buffer = bufferDic[partitionId];

                            if (cancellationTokenSource.Token.IsCancellationRequested)
                            {
                                if (buffer.Any())
                                {
                                    Console.WriteLine($"Dump {buffer.Count} messages from partition {partitionId} at {checkTime:o}.");
                                    totalReceived += buffer.Count();
                                    buffer.Clear();
                                }
                                break;
                            }

                            DateTimeOffset lastDumpTime =
                                lastDumpTimeDic[partitionId];

                            double secondsSinceLastDump =
                                checkTime.Subtract(lastDumpTime).TotalSeconds;

                            if (buffer.Any()
                                && secondsSinceLastDump >= partionInfos.Count()
                                && (secondsSinceLastDump % partionInfos.Count()) == 0)
                            {
                                IEventData checkpointData = null;

                                lock (buffer)
                                {
                                    Console.WriteLine($"Dump {buffer.Count} messages from partition {partitionId} at {checkTime:o}.");
                                    totalReceived += buffer.Count();

                                    checkpointData =
                                        buffer
                                            .OrderByDescending(e => e.SequenceNumber)
                                            .FirstOrDefault();
                                    buffer.Clear();
                                }

                                if (checkpointData != null)
                                {
                                    IEventHubPartitionContext partitionContext =
                                        partitionContextDic[partitionId];
                                    await partitionContext
                                        .CheckpointAsync(checkpointData).ConfigureAwait(false);
                                }

                                lastDumpTimeDic[partitionId] = checkTime;

                                // Console.WriteLine($"last dump time for partition {partitionId}: {lastDumpTimeDic[partitionId]}");
                            }
                            else
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(1));
                            }
                        }
                    });

                tasksList.Add(task);
            }

            Task eventProcessorHandler(object obj, EventProcessorEventArgs args)
            {
                // Console.WriteLine($"[Partition - {args.PartitionContext.PartitionId}] [{args.EventType}]");
                // this.PrintObjectInfo(args);

                if (args.EventType == EventProcessorEventType.DataReceived)
                {
                    string partitionId = args.PartitionContext.PartitionId;

                    partitionContextDic[partitionId] = args.PartitionContext;

                    List<IEventData> buffer = bufferDic[partitionId];

                    lock (buffer)
                    {
                        Console.WriteLine($"Buffered {args.DataCollection.Count()} messages from partition {partitionId} at {DateTimeOffset.UtcNow:o}.");
                        buffer.AddRange(args.DataCollection);
                    }
                }

                return Task.CompletedTask;
            }

            eventProcessor.OnClosed += eventProcessorHandler;
            eventProcessor.OnOpened += eventProcessorHandler;
            eventProcessor.OnErrorOccurred += eventProcessorHandler;
            eventProcessor.OnDataReceived += eventProcessorHandler;

            IEventProcessorRegistration registration =
                await eventHubClient.RegisterAsync(eventProcessor)
                    .ConfigureAwait(false);

            char c;
            do
            {
                Console.WriteLine("Receiving event data, press 'q' to exit.");
                c = Console.ReadKey(true).KeyChar;
            } while(c != 'q');

            cancellationTokenSource.Cancel();
            Task.WaitAll(tasksList.ToArray());

            Console.WriteLine($"Total received: {totalReceived}");

            await registration.UnregisterAsync().ConfigureAwait(false);
        }
    }
}