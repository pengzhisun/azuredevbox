namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;

    public interface IEventData
    {
        byte[] BodyBytes { get; }

        string BodyString { get; }

        IReadOnlyDictionary<string, object> Properties { get; }

        long SequenceNumber { get; }

        DateTime EnqueuedTimeUtc { get; }

        string Offset { get; }

        string PartitionKey { get; }
    }
}