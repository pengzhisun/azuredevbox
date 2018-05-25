//------------------------------------------------------------------------------
// <copyright file="EventHubReceiverRuntimeInformation.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    using AzureReceiverRuntimeInformation = Microsoft.Azure.EventHubs.ReceiverRuntimeInformation;

    /// <summary>
    /// Defines the Event Hub receiver runtime information class.
    /// </summary>
    public sealed class EventHubReceiverRuntimeInformation
        : IEventHubReceiverRuntimeInformation
    {
        #region Fields

        /// <summary>
        /// The internal Azure Event Hub receiver runtime information instance.
        /// </summary>
        private readonly AzureReceiverRuntimeInformation azureRuntimeInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubReceiverRuntimeInformation"/> class.
        /// </summary>
        /// <param name="azureReceiverRuntimeInformation">The <see cref="AzureReceiverRuntimeInformation"/> instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="AzureReceiverRuntimeInformation"/> instance is null.
        /// </exception>
        public EventHubReceiverRuntimeInformation(
            AzureReceiverRuntimeInformation azureReceiverRuntimeInformation)
        {
            Checks.Parameter(
                nameof(azureReceiverRuntimeInformation),
                azureReceiverRuntimeInformation)
                .NotNull();

            this.azureRuntimeInfo = azureReceiverRuntimeInformation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the partition identifier for a logical partition of the Event
        /// Hub.
        /// </summary>
        public string PartitionId => this.azureRuntimeInfo.PartitionId;

        /// <summary>
        /// Gets the sequence number of the last event within the Event Hub
        /// partition stream.
        /// </summary>
        public long LastSequenceNumber
            => this.azureRuntimeInfo.LastSequenceNumber;

        /// <summary>
        /// Gets the enqueued UTC time of the last event within the Event Hub
        /// partition stream.
        /// </summary>
        public DateTime LastEnqueuedTimeUtc
            => this.azureRuntimeInfo.LastEnqueuedTimeUtc;

        /// <summary>
        /// Gets the offset of the last event within the Event Hub partition
        /// stream.
        /// </summary>
        public string LastEnqueuedOffset
            => this.azureRuntimeInfo.LastEnqueuedOffset;

        /// <summary>
        /// Gets the time of when the runtime infomation was retrieved.
        /// </summary>
        public DateTime RetrievalTime => this.azureRuntimeInfo.RetrievalTime;

        #endregion
    }
}