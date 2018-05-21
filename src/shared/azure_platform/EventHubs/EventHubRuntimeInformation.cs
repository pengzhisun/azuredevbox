namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AzureEventHubRuntimeInformation = Microsoft.Azure.EventHubs.EventHubRuntimeInformation;

    /// <summary>
    /// Defines the event hub runtime information class.
    /// </summary>
    /// <seealso cref="IEventHubRuntimeInformation" />
    public sealed class EventHubRuntimeInformation : IEventHubRuntimeInformation
    {
        #region Fields

        /// <summary>
        /// The internal Azure event hub runtime information
        /// </summary>
        private readonly AzureEventHubRuntimeInformation internalRuntimeInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubRuntimeInformation"/> class.
        /// </summary>
        /// <param name="runtimeInformation">The Azure event hub runtime information.</param>
        public EventHubRuntimeInformation(
            AzureEventHubRuntimeInformation runtimeInformation)
        {
            this.internalRuntimeInfo = runtimeInformation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path => this.internalRuntimeInfo.Path;

        /// <summary>
        /// Gets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt => this.internalRuntimeInfo.CreatedAt;

        /// <summary>
        /// Gets the partition count.
        /// </summary>
        /// <value>
        /// The partition count.
        /// </value>
        public int PartitionCount => this.internalRuntimeInfo.PartitionCount;

        /// <summary>
        /// Gets the partition ids.
        /// </summary>
        /// <value>
        /// The partition ids.
        /// </value>
        public IEnumerable<string> PartitionIds
            => this.internalRuntimeInfo.PartitionIds.Skip(0);

        #endregion
    }
}
