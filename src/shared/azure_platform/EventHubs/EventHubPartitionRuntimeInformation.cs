namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    using AzureEventHubPartitionRuntimeInformation = Microsoft.Azure.EventHubs.EventHubPartitionRuntimeInformation;

    /// <summary>
    /// Defines the event hub partition runtime information class.
    /// </summary>
    /// <seealso cref="IEventHubPartitionRuntimeInformation" />
    public sealed class EventHubPartitionRuntimeInformation
        : IEventHubPartitionRuntimeInformation
    {
        #region Fields

        /// <summary>
        /// The internal Azure event hub partition information.
        /// </summary>
        private readonly AzureEventHubPartitionRuntimeInformation internalPartitionInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubPartitionRuntimeInformation"/> class.
        /// </summary>
        /// <param name="partitionInformation">The Azure event hub partition information.</param>
        public EventHubPartitionRuntimeInformation(
            AzureEventHubPartitionRuntimeInformation partitionInformation)
        {
            this.internalPartitionInfo = partitionInformation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path => this.internalPartitionInfo.Path;

        /// <summary>
        /// Gets the partition identifier.
        /// </summary>
        /// <value>
        /// The partition identifier.
        /// </value>
        public string PartitionId => this.internalPartitionInfo.PartitionId;

        /// <summary>
        /// Gets the begin sequence number.
        /// </summary>
        /// <value>
        /// The begin sequence number.
        /// </value>
        public long BeginSequenceNumber
            => this.internalPartitionInfo.BeginSequenceNumber;

        /// <summary>
        /// Gets the last enqueued sequence number.
        /// </summary>
        /// <value>
        /// The last enqueued sequence number.
        /// </value>
        public long LastEnqueuedSequenceNumber
            => this.internalPartitionInfo.LastEnqueuedSequenceNumber;

        /// <summary>
        /// Gets the last enqueued offset.
        /// </summary>
        /// <value>
        /// The last enqueued offset.
        /// </value>
        public string LastEnqueuedOffset
            => this.internalPartitionInfo.LastEnqueuedOffset;

        /// <summary>
        /// Gets the last enqueued time UTC.
        /// </summary>
        /// <value>
        /// The last enqueued time UTC.
        /// </value>
        public DateTime LastEnqueuedTimeUtc
            => this.internalPartitionInfo.LastEnqueuedTimeUtc;

        #endregion
    }
}
