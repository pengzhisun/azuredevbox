namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    using AzureEventData = Microsoft.Azure.EventHubs.EventData;

    public sealed class EventData : IEventData
    {
        private readonly AzureEventData azureEventData;

        private readonly Lazy<IDictionary<string, object>> lazyProperties;

        private readonly Lazy<string> lazyBodyString;

        public EventData(AzureEventData azureEventData)
        {
            this.azureEventData = azureEventData;

            this.lazyProperties =
                new Lazy<IDictionary<string, object>>(
                    () => new ReadOnlyDictionary<string, object>(
                        this.azureEventData.Properties));

            this.lazyBodyString =
                new Lazy<string>(() => Encoding.UTF8.GetString(this.BodyBytes));
        }

        public byte[] BodyBytes => this.azureEventData.Body.Array;

        public string BodyString => this.lazyBodyString.Value;

        public IDictionary<string, object> Properties
            => this.lazyProperties.Value;

        public long SequenceNumber
            => this.azureEventData.SystemProperties.SequenceNumber;

        public DateTime EnqueuedTimeUtc
            => this.azureEventData.SystemProperties.EnqueuedTimeUtc;

        public string Offset => this.azureEventData.SystemProperties.Offset;

        public string PartitionKey
            => this.azureEventData.SystemProperties.TryGetValue(
                "x-opt-partition-key",
                out object partitionKey)
                    ? partitionKey.ToString() : null;

        internal AzureEventData AzureEventData => this.azureEventData;
    }
}
