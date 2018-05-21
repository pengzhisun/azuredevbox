namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    /// <summary>
    /// Defines the event hub partition runtime information interface.
    /// </summary>
    public interface IEventHubPartitionRuntimeInformation
    {
        #region Properties

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        string Path { get; }

        /// <summary>
        /// Gets the partition identifier.
        /// </summary>
        /// <value>
        /// The partition identifier.
        /// </value>
        string PartitionId { get; }

        /// <summary>
        /// Gets the begin sequence number.
        /// </summary>
        /// <value>
        /// The begin sequence number.
        /// </value>
        long BeginSequenceNumber { get; }

        /// <summary>
        /// Gets the last enqueued sequence number.
        /// </summary>
        /// <value>
        /// The last enqueued sequence number.
        /// </value>
        long LastEnqueuedSequenceNumber { get; }

        /// <summary>
        /// Gets the last enqueued offset.
        /// </summary>
        /// <value>
        /// The last enqueued offset.
        /// </value>
        string LastEnqueuedOffset { get; }

        /// <summary>
        /// Gets the last enqueued time UTC.
        /// </summary>
        /// <value>
        /// The last enqueued time UTC.
        /// </value>
        DateTime LastEnqueuedTimeUtc { get; }

        #endregion
    }
}
