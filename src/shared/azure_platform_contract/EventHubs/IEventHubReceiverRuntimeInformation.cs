namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    public interface IEventHubReceiverRuntimeInformation
    {
        string PartitionId { get; }

        long LastSequenceNumber { get; }

        DateTime LastEnqueuedTimeUtc { get; }

        string LastEnqueuedOffset { get; }

        DateTime RetrievalTime { get; }
    }
}