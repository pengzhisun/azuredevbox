namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class EventProcessor : IEventProcessor
    {
        public EventProcessor(
            string consumerGroupName,
            string storageAccountConnectionString,
            string containerName)
        {
            this.ConsumerGroupName = consumerGroupName;
            this.StorageAccountConnectionString = storageAccountConnectionString;
            this.ContainerName = containerName;
        }

        public string ConsumerGroupName { get; private set; }

        public string StorageAccountConnectionString { get; private set; }

        public string ContainerName { get; private set; }

        public IEventProcessorOptions Options { get; set; }

        public event EventProcessorAsyncEventHandler OnClosed;

        public event EventProcessorAsyncEventHandler OnOpened;

        public event EventProcessorAsyncEventHandler OnErrorOccurred;

        public event EventProcessorAsyncEventHandler OnDataReceived;

        public async Task CloseAsync(
            IEventHubPartitionContext partitionContext,
            EventProcessorCloseReason closeReason)
        {
            Lazy<EventProcessorEventArgs> lazyEventArgs =
                new Lazy<EventProcessorEventArgs>(
                    () =>
                    new EventProcessorEventArgs(
                        EventProcessorEventType.Closed,
                        partitionContext,
                        closeReason: closeReason));

            await this.InvokeEventHandler(this.OnClosed, lazyEventArgs);
        }

        public async Task OpenAsync(IEventHubPartitionContext partitionContext)
        {
            Lazy<EventProcessorEventArgs> lazyEventArgs =
                  new Lazy<EventProcessorEventArgs>(
                      () =>
                      new EventProcessorEventArgs(
                          EventProcessorEventType.Opened,
                          partitionContext));

            await this.InvokeEventHandler(this.OnOpened, lazyEventArgs);
        }

        public async Task ProcessErrorAsync(
            IEventHubPartitionContext partitionContext,
            Exception error)
        {
            Lazy<EventProcessorEventArgs> lazyEventArgs =
                  new Lazy<EventProcessorEventArgs>(
                      () =>
                      new EventProcessorEventArgs(
                          EventProcessorEventType.ErrorOccurred,
                          partitionContext,
                          exception: error));

            await this.InvokeEventHandler(this.OnErrorOccurred, lazyEventArgs);
        }

        public async Task ProcessEventsAsync(
            IEventHubPartitionContext partitionContext,
            IEnumerable<IEventData> messages)
        {
            Lazy<EventProcessorEventArgs> lazyEventArgs =
                   new Lazy<EventProcessorEventArgs>(
                       () =>
                       new EventProcessorEventArgs(
                           EventProcessorEventType.DataReceived,
                           partitionContext,
                           dataCollection: messages));

            await this.InvokeEventHandler(this.OnDataReceived, lazyEventArgs);
        }

        private async Task InvokeEventHandler(
            EventProcessorAsyncEventHandler eventHandler,
            Lazy<EventProcessorEventArgs> lazyEventArgs)
        {
            EventProcessorAsyncEventHandler localEventHandler = eventHandler;

            if (localEventHandler != null)
            {
                EventProcessorEventArgs eventArgs = lazyEventArgs.Value;

                await Task.WhenAll(
                    localEventHandler.GetInvocationList()
                        .OfType<EventProcessorAsyncEventHandler>()
                        .Select(func => func.Invoke(this, eventArgs)));
            }
        }
    }
}
