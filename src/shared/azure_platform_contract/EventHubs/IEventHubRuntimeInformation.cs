namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the event hub runtime information interface.
    /// </summary>
    public interface IEventHubRuntimeInformation
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
        /// Gets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the partition count.
        /// </summary>
        /// <value>
        /// The partition count.
        /// </value>
        int PartitionCount { get; }

        /// <summary>
        /// Gets the partition ids.
        /// </summary>
        /// <value>
        /// The partition ids.
        /// </value>
        IEnumerable<string> PartitionIds { get; }

        #endregion
    }
}
