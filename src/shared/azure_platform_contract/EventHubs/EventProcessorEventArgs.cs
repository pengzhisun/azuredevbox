namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public sealed class EventProcessorEventArgs : EventArgs
    {
        public EventProcessorEventArgs(
            EventProcessorEventType eventType,
            IEventHubPartitionContext partitionContext,
            EventProcessorCloseReason? closeReason = null,
            Exception exception = null,
            IEnumerable<IEventData> dataCollection = null)
        {
            this.EventType = eventType;
            this.PartitionContext = partitionContext;
            this.CloseReason = closeReason;
            this.Exception = exception;
            this.DataCollection = dataCollection;
        }

        public IEventHubPartitionContext PartitionContext { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EventProcessorEventType EventType { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EventProcessorCloseReason? CloseReason { get; private set; }

        public Exception Exception { get; private set; }

        public IEnumerable<IEventData> DataCollection { get; private set; }
    }
}
