namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    public interface IEventProcessorOptions
    {
        int? MaxBatchSize { get; set; }

        bool? EnableReceiverRuntimeMetric { get; set; }

        TimeSpan? ReceiveTimeout { get; set; }

        int? PrefetchCount { get; set; }

        bool? InvokeProcessorAfterReceiveTimeout { get; set; }

        Func<string, IEventPosition> InitialOffsetProvider { get; set; }

        event EventHandler<IExceptionReceivedEventArgs> OnExceptionReceived;
    }
}