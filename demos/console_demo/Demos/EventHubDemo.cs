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

            int batchCount = 5;
            int batchSize = 10;
            for (int batchIdx = 0; batchIdx < batchCount; ++batchIdx)
            {
                await eventHubClient
                    .BatchSendAsync(
                        new int[batchSize].Select(
                            (v, i) => $"Data {batchIdx}-{i} at {DateTime.UtcNow:o}"))
                    .ConfigureAwait(false);
            }
            Console.WriteLine($"Total sent: {batchCount * batchSize}");

            IEventProcessor eventProcessor =
                 new EventProcessor(
                   this.EventHubConsumerGroup,
                   this.StorageAccountConnectionString,
                   this.StorageAccountContainer);

            int totalReceived = 0;

            async Task eventProcessorHandler(object obj, EventProcessorEventArgs args)
            {
                Console.WriteLine($"[Partition - {args.PartitionContext.PartitionId}] [{args.EventType}]");
                this.PrintObjectInfo(args);

                if (args.EventType == EventProcessorEventType.DataReceived)
                {
                    totalReceived += args.DataCollection.Count();
                    IEventData eventData =
                        args.DataCollection
                            .OrderByDescending(e => e.SequenceNumber)
                            .FirstOrDefault();

                    if (eventData != null)
                    {
                        await args.PartitionContext
                            .CheckpointAsync(eventData).ConfigureAwait(false);
                    }
                }
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

            Console.WriteLine($"Total received: {totalReceived}");

            await registration.UnregisterAsync().ConfigureAwait(false);
        }
    }
}