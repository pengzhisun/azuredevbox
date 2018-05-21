namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventProcessor
    {
        string ConsumerGroupName { get; }

        string StorageAccountConnectionString { get; }

        string ContainerName { get; }

        event EventProcessorAsyncEventHandler OnClosed;

        event EventProcessorAsyncEventHandler OnOpened;

        event EventProcessorAsyncEventHandler OnErrorOccurred;

        event EventProcessorAsyncEventHandler OnDataReceived;

        Task CloseAsync(
            IEventHubPartitionContext partitionContext,
            EventProcessorCloseReason closeReason);

        Task OpenAsync(IEventHubPartitionContext partitionContext);

        Task ProcessErrorAsync(
            IEventHubPartitionContext partitionContext,
            Exception error);

        Task ProcessEventsAsync(
            IEventHubPartitionContext partitionContext,
            IEnumerable<IEventData> messages);
    }
}