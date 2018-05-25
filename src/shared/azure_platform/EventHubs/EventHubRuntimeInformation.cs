//------------------------------------------------------------------------------
// <copyright file="EventHubRuntimeInformation.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

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
    internal sealed class EventHubRuntimeInformation : IEventHubRuntimeInformation
    {
        #region Fields

        /// <summary>
        /// The internal Azure Event Hub runtime information.
        /// </summary>
        private readonly AzureEventHubRuntimeInformation azureRuntimeInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubRuntimeInformation"/> class.
        /// </summary>
        /// <param name="azureRuntimeInformation">The <see cref="AzureEventHubRuntimeInformation" /> instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="AzureEventHubRuntimeInformation"/> instance is null.
        /// </exception>
        public EventHubRuntimeInformation(
            AzureEventHubRuntimeInformation azureRuntimeInformation)
        {
            Checks.Parameter(
                nameof(azureRuntimeInformation),
                azureRuntimeInformation)
                .NotNull();

            this.azureRuntimeInfo = azureRuntimeInformation;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the path to the Event Hub.
        /// </summary>
        public string Path => this.azureRuntimeInfo.Path;

        /// <summary>
        /// Gets the time at which the Event Hub was created.
        /// </summary>
        public DateTime CreatedAt => this.azureRuntimeInfo.CreatedAt;

        /// <summary>
        /// Gets the number of partitions in an Event Hub.
        /// </summary>
        public int PartitionCount => this.azureRuntimeInfo.PartitionCount;

        /// <summary>
        /// Gets the partition IDs for an Event Hub.
        /// </summary>
        public IEnumerable<string> PartitionIds
            => this.azureRuntimeInfo.PartitionIds.Skip(0);

        #endregion
    }
}
