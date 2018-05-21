namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AzureEventHubsConnectionStringBuilder = Microsoft.Azure.EventHubs.EventHubsConnectionStringBuilder;
    using AzureEventHubClient = Microsoft.Azure.EventHubs.EventHubClient;
    using AzureEventData = Microsoft.Azure.EventHubs.EventData;
    using AzureEventDataBatch = Microsoft.Azure.EventHubs.EventDataBatch;

    /// <summary>
    /// Defines the event hub client.
    /// </summary>
    /// <seealso cref="IEventHubClient" />
    public sealed class EventHubClient : IEventHubClient
    {
        #region Fields

        /// <summary>
        /// The internal Azure event hub client in lazy initialization mode.
        /// </summary>
        private readonly Lazy<AzureEventHubClient> lazyAzureEventHubClient;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubClient"/> class.
        /// </summary>
        /// <param name="eventHubConnectionString">The event hub connection string.</param>
        /// <param name="entityPath">The entity path.</param>
        public EventHubClient(string eventHubConnectionString, string entityPath)
        {
            this.EventHubConnectionString = eventHubConnectionString;
            this.EntityPath = entityPath;

            this.lazyAzureEventHubClient =
                new Lazy<AzureEventHubClient>(CreateAzureEventHubClient);
        }

        #endregion

        public string EventHubConnectionString { get; private set; }

        public string EntityPath { get; private set; }

        public string ClientId => this.AzureEventHubClient.ClientId;

        public string EventHubName => this.AzureEventHubClient.EventHubName;

        private AzureEventHubClient AzureEventHubClient
            => this.lazyAzureEventHubClient.Value;

        public async Task<IEventHubRuntimeInformation> GetEventHubRuntimeInformationAsync()
        {
            var runtimeInformation =
                await this.AzureEventHubClient.GetRuntimeInformationAsync();

            return new EventHubRuntimeInformation(runtimeInformation);
        }

        public async Task<IEventHubPartitionRuntimeInformation> GetPartitionRuntimeInformationAsync(
            string partitionId)
        {
            var partitionInformation =
                await this.AzureEventHubClient.GetPartitionRuntimeInformationAsync(
                    partitionId);

            return new EventHubPartitionRuntimeInformation(partitionInformation);
        }

        public async Task BatchSendAsync(IEnumerable<string> messages)
            => await this.BatchSendAsync(
                messages.Select(m => Encoding.UTF8.GetBytes(m)));

        public async Task BatchSendAsync(IEnumerable<byte[]> messages)
        {
            AzureEventDataBatch batch = this.AzureEventHubClient.CreateBatch();

            foreach (byte[] messageBytes in messages)
            {
                if (!batch.TryAdd(new AzureEventData(messageBytes)))
                {
                    throw new EventHubException
                    {
                        ErrorCode = EventHubErrorCode.ExceedBatchSizeLimitation
                    };
                }
            }

            await this.AzureEventHubClient.SendAsync(batch);
        }

        public async Task<IEventProcessorRegistration> RegisterAsync(
            IEventProcessor eventProcessor)
        {
            IEventProcessorRegistration registration =
                new EventProcessorRegistration(eventProcessor);

            await registration.RegisterAsync(
                this.EventHubConnectionString,
                this.EntityPath);

            return registration;
        }

        private AzureEventHubClient CreateAzureEventHubClient()
        {
            AzureEventHubsConnectionStringBuilder connectionStringBuilder =
                new AzureEventHubsConnectionStringBuilder(
                    this.EventHubConnectionString)
                {
                    EntityPath = this.EntityPath
                };

            string connectionString = connectionStringBuilder.ToString();

            var azureEventClient =
                AzureEventHubClient.CreateFromConnectionString(connectionString);

            return azureEventClient;
        }
    }
}
