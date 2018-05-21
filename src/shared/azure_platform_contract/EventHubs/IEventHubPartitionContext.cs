namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventHubPartitionContext
    {
       CancellationToken CancellationToken { get; }

       string ConsumerGroupName { get; }

       string EventHubPath { get; }

       string PartitionId { get; }

       string Owner { get; }

       IEventHubReceiverRuntimeInformation RuntimeInformation { get; }

       Task CheckpointAsync();

       Task CheckpointAsync(IEventData eventData);
    }
}