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
    internal sealed class EventHubReceiverRuntimeInformation
        : IEventHubReceiverRuntimeInformation
    {
        #region Fields

        /// <summary>
        /// The internal Azure Event Hub receiver runtime information instance.
        /// </summary>
        private readonly AzureReceiverRuntimeInformation azureReceiverInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubReceiverRuntimeInformation"/> class.
        /// </summary>
        /// <param name="azureReceiverInformation">The <see cref="AzureReceiverRuntimeInformation"/> instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="AzureReceiverRuntimeInformation"/> instance is null.
        /// </exception>
        public EventHubReceiverRuntimeInformation(
            AzureReceiverRuntimeInformation azureReceiverInformation)
        {
            Checks.Parameter(
                nameof(azureReceiverInformation),
                azureReceiverInformation)
                .NotNull();

            this.azureReceiverInfo = azureReceiverInformation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the partition identifier for a logical partition of the Event
        /// Hub.
        /// </summary>
        public string PartitionId => this.azureReceiverInfo.PartitionId;

        /// <summary>
        /// Gets the sequence number of the last event within the Event Hub
        /// partition stream.
        /// </summary>
        public long LastSequenceNumber
            => this.azureReceiverInfo.LastSequenceNumber;

        /// <summary>
        /// Gets the enqueued UTC time of the last event within the Event Hub
        /// partition stream.
        /// </summary>
        public DateTime LastEnqueuedTimeUtc
            => this.azureReceiverInfo.LastEnqueuedTimeUtc;

        /// <summary>
        /// Gets the offset of the last event within the Event Hub partition
        /// stream.
        /// </summary>
        public string LastEnqueuedOffset
            => this.azureReceiverInfo.LastEnqueuedOffset;

        /// <summary>
        /// Gets the time of when the runtime infomation was retrieved.
        /// </summary>
        public DateTime RetrievalTime => this.azureReceiverInfo.RetrievalTime;

        #endregion
    }
}