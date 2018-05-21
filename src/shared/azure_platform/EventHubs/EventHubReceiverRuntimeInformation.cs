namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    using AzureReceiverRuntimeInformation = Microsoft.Azure.EventHubs.ReceiverRuntimeInformation;

    public sealed class EventHubReceiverRuntimeInformation
        : IEventHubReceiverRuntimeInformation
    {
        private readonly AzureReceiverRuntimeInformation azureRuntimeInfo;

        public EventHubReceiverRuntimeInformation(
            AzureReceiverRuntimeInformation azureReceiverRuntimeInformation)
        {
            this.azureRuntimeInfo = azureReceiverRuntimeInformation;
        }

        public string PartitionId => this.azureRuntimeInfo.PartitionId;

        public long LastSequenceNumber
            => this.azureRuntimeInfo.LastSequenceNumber;

        public DateTime LastEnqueuedTimeUtc
            => this.azureRuntimeInfo.LastEnqueuedTimeUtc;

        public string LastEnqueuedOffset
            => this.azureRuntimeInfo.LastEnqueuedOffset;

        public DateTime RetrievalTime => this.azureRuntimeInfo.RetrievalTime;
    }
}