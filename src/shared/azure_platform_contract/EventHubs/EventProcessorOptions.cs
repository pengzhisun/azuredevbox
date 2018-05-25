namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    public sealed class EventProcessorOptions
    {
        public int MaxBatchSize { get; set; }

        public bool EnableReceiverRuntimeMetric { get; set; }

        public TimeSpan ReceiveTimeout { get; set; }

        public int PrefetchCount { get; set; }

        public bool InvokeProcessorAfterReceiveTimeout { get; set; }

        public Func<string, IEventPosition> InitialOffsetProvider { get; set; }
    }
}