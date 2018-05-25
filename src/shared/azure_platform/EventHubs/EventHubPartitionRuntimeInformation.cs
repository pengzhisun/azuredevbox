//------------------------------------------------------------------------------
// <copyright file="EventHubPartitionRuntimeInformation.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    using AzureEventHubPartitionRuntimeInformation = Microsoft.Azure.EventHubs.EventHubPartitionRuntimeInformation;

    /// <summary>
    /// Defines the Event Hub partition runtime information class.
    /// </summary>
    /// <seealso cref="IEventHubPartitionRuntimeInformation" />
    internal sealed class EventHubPartitionRuntimeInformation
        : IEventHubPartitionRuntimeInformation
    {
        #region Fields

        /// <summary>
        /// The internal Azure Event Hub partition information.
        /// </summary>
        private readonly AzureEventHubPartitionRuntimeInformation azurePartitionInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubPartitionRuntimeInformation"/> class.
        /// </summary>
        /// <param name="azurePartitionInformation">The <see cref="AzureEventHubPartitionRuntimeInformation" /> instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="AzureEventHubPartitionRuntimeInformation"/> instance is null.
        /// </exception>
        public EventHubPartitionRuntimeInformation(
            AzureEventHubPartitionRuntimeInformation azurePartitionInformation)
        {
            Checks.Parameter(
                nameof(azurePartitionInformation),
                azurePartitionInformation)
                .NotNull();

            this.azurePartitionInfo = azurePartitionInformation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path of the Event Hub.
        /// </summary>
        public string Path => this.azurePartitionInfo.Path;

        /// <summary>
        /// Gets the partition identifier for a logical partition of the Event
        /// Hub.
        /// </summary>
        public string PartitionId => this.azurePartitionInfo.PartitionId;

        /// <summary>
        /// Gets the begin sequence number within the Event Hub partition stream.
        /// </summary>
        public long BeginSequenceNumber
            => this.azurePartitionInfo.BeginSequenceNumber;

        /// <summary>
        /// Gets the end sequence number within the Event Hub partition stream.
        /// </summary>
        public long LastEnqueuedSequenceNumber
            => this.azurePartitionInfo.LastEnqueuedSequenceNumber;

        /// <summary>
        /// Gets the offset of the last enqueued event.
        /// </summary>
        public string LastEnqueuedOffset
            => this.azurePartitionInfo.LastEnqueuedOffset;

        /// <summary>
        /// Gets the enqueued UTC time of the last event.
        /// </summary>
        public DateTime LastEnqueuedTimeUtc
            => this.azurePartitionInfo.LastEnqueuedTimeUtc;

        #endregion
    }
}
