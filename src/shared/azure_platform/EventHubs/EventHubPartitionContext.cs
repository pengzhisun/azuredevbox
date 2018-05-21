namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AzureEventData = Microsoft.Azure.EventHubs.EventData;
    using AzurePartitionContext = Microsoft.Azure.EventHubs.Processor.PartitionContext;

    public sealed class EventHubPartitionContext : IEventHubPartitionContext
    {
        private readonly AzurePartitionContext azurePartitionContext;
        private readonly Lazy<EventHubReceiverRuntimeInformation> lazyReceiverRuntimneInfo;

        public EventHubPartitionContext(
            AzurePartitionContext azurePartitionContext)
        {
            this.azurePartitionContext = azurePartitionContext;

            this.lazyReceiverRuntimneInfo =
                new Lazy<EventHubReceiverRuntimeInformation>(
                    () => new EventHubReceiverRuntimeInformation(
                        this.azurePartitionContext.RuntimeInformation));
        }

        public CancellationToken CancellationToken
            => this.azurePartitionContext.CancellationToken;

        public string ConsumerGroupName
            => this.azurePartitionContext.ConsumerGroupName;

        public string EventHubPath => this.azurePartitionContext.EventHubPath;

        public string PartitionId => this.azurePartitionContext.PartitionId;

        public string Owner => this.azurePartitionContext.Owner;

        public IEventHubReceiverRuntimeInformation RuntimeInformation
            => this.lazyReceiverRuntimneInfo.Value;

        public async Task CheckpointAsync()
        {
            await this.azurePartitionContext.CheckpointAsync();
        }

        public async Task CheckpointAsync(IEventData eventData)
        {
            AzureEventData azureEventData = eventData.ToAzureEventData();

            await this.azurePartitionContext.CheckpointAsync(azureEventData);
        }
    }
}
