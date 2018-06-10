namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AzureEventData = Microsoft.Azure.EventHubs.EventData;
    using AzurePartitionContext = Microsoft.Azure.EventHubs.Processor.PartitionContext;
    using AzureEventProcessorHost = Microsoft.Azure.EventHubs.Processor.EventProcessorHost;
    using AzureCloseReason = Microsoft.Azure.EventHubs.Processor.CloseReason;
    using IAzureEventProcessor = Microsoft.Azure.EventHubs.Processor.IEventProcessor;
    using IAzureEventProcessorFactory = Microsoft.Azure.EventHubs.Processor.IEventProcessorFactory;

    public sealed class EventProcessorRegistration : IEventProcessorRegistration
    {
        private int disposed = 0;
        private AzureEventProcessorHost azureEventProcessorHost;

        public EventProcessorRegistration(IEventProcessor eventProcessor)
        {
            this.EventProcessor = eventProcessor;
        }

        public IEventProcessor EventProcessor { get; private set; }

        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref this.disposed, 1, 0) == 0)
            {
                this.UnregisterAsync()
                    .ConfigureAwait(false).GetAwaiter().GetResult();
                GC.SuppressFinalize(this);
            }
        }

        public async Task RegisterAsync(
            string eventHubConnectionString,
            string eventHubPath)
        {
            AzureEventProcessorHost processorHost =
               new AzureEventProcessorHost(
                   eventHubPath,
                   this.EventProcessor.ConsumerGroupName,
                   eventHubConnectionString,
                   this.EventProcessor.StorageAccountConnectionString,
                   this.EventProcessor.ContainerName);

            AzureEventProcessorFactory azureEventProcessorFactory =
                new AzureEventProcessorFactory(this.EventProcessor);

            await processorHost.RegisterEventProcessorFactoryAsync(
                azureEventProcessorFactory,
                this.EventProcessor.Options.ToAzureOptions());

            this.azureEventProcessorHost = processorHost;
        }

        public async Task UnregisterAsync()
        {
            if (this.azureEventProcessorHost != null)
            {
                await this.azureEventProcessorHost
                    .UnregisterEventProcessorAsync();
                this.azureEventProcessorHost = null;
            }
        }

        private sealed class AzureEventProcessorFactory
            : IAzureEventProcessorFactory
        {
            private readonly IEventProcessor eventProcessor;

            public AzureEventProcessorFactory(IEventProcessor eventProcessor)
            {
                this.eventProcessor = eventProcessor;
            }

            public IAzureEventProcessor CreateEventProcessor(
                AzurePartitionContext context)
            {
                IEventHubPartitionContext partitionContext =
                    new EventHubPartitionContext(context);

                AzureEventProcessor azureEventProcessor =
                    new AzureEventProcessor(this.eventProcessor);

                return azureEventProcessor;
            }
        }

        private sealed class AzureEventProcessor : IAzureEventProcessor
        {
            private readonly IEventProcessor eventProcessor;

            public AzureEventProcessor(IEventProcessor eventProcessor)
            {
                this.eventProcessor = eventProcessor;
            }

            public async Task CloseAsync(
                AzurePartitionContext context,
                AzureCloseReason reason)
            {
                IEventHubPartitionContext partitionContext =
                    new EventHubPartitionContext(context);
                EventProcessorCloseReason closeReason =
                    reason.ToEventProcessorCloseReason();

                await this.eventProcessor.CloseAsync(
                    partitionContext,
                    closeReason);
            }

            public async Task OpenAsync(AzurePartitionContext context)
            {
                IEventHubPartitionContext partitionContext =
                     new EventHubPartitionContext(context);

                await this.eventProcessor.OpenAsync(partitionContext);
            }

            public async Task ProcessErrorAsync(
                AzurePartitionContext context,
                Exception error)
            {
                IEventHubPartitionContext partitionContext =
                     new EventHubPartitionContext(context);

                await this.eventProcessor.ProcessErrorAsync(
                    partitionContext,
                    error);
            }

            public async Task ProcessEventsAsync(
                AzurePartitionContext context,
                IEnumerable<AzureEventData> messages)
            {
                IEventHubPartitionContext partitionContext =
                    new EventHubPartitionContext(context);

                IEnumerable<IEventData> eventDatas =
                    messages.Select(
                        azureEventData => new EventData(azureEventData));

                await this.eventProcessor.ProcessEventsAsync(
                    partitionContext,
                    eventDatas);
            }
        }
    }
}
