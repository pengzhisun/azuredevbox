
//------------------------------------------------------------------------------
// <copyright file="EventHubPartitionContext.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AzureEventData = Microsoft.Azure.EventHubs.EventData;
    using AzurePartitionContext = Microsoft.Azure.EventHubs.Processor.PartitionContext;

    /// <summary>
    /// Defines the Azure Event Hub partition context class which used by
    /// <see cref="IEventProcessor" />.
    /// </summary>
    internal sealed class EventHubPartitionContext : IEventHubPartitionContext
    {
        #region Fields

        /// <summary>
        /// The internal Azure Event Hub partition context instance.
        /// </summary>
        private readonly AzurePartitionContext azurePartitionContext;

        /// <summary>
        /// The internal Azure Event Hub receiver runtime information lazy
        /// instance.
        /// </summary>
        private readonly Lazy<EventHubReceiverRuntimeInformation> lazyReceiverRuntimneInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHubPartitionContext"/> class.
        /// </summary>
        /// <param name="azurePartitionContext">The <see cref="AzurePartitionContext"/> instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="AzurePartitionContext"/> instance is null.
        /// </exception>
        public EventHubPartitionContext(
            AzurePartitionContext azurePartitionContext)
        {
            Checks.Parameter(
                nameof(azurePartitionContext),
                azurePartitionContext)
                .NotNull();

            this.azurePartitionContext = azurePartitionContext;

            this.lazyReceiverRuntimneInfo =
                new Lazy<EventHubReceiverRuntimeInformation>(
                    () => new EventHubReceiverRuntimeInformation(
                        this.azurePartitionContext.RuntimeInformation));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets triggered when the partition gets closed.
        /// </summary>
        public CancellationToken CancellationToken
            => this.azurePartitionContext.CancellationToken;

        /// <summary>
        /// Gets the name of the consumer group.
        /// </summary>
        public string ConsumerGroupName
            => this.azurePartitionContext.ConsumerGroupName;

        /// <summary>
        /// Gets the path of the Event Hub.
        /// </summary>
        public string EventHubPath => this.azurePartitionContext.EventHubPath;

        /// <summary>
        /// Gets the partition ID for the context.
        /// </summary>
        public string PartitionId => this.azurePartitionContext.PartitionId;

        /// <summary>
        /// Gets the host owner for the partition.
        /// </summary>
        public string Owner => this.azurePartitionContext.Owner;

        /// <summary>
        /// Gets the approximate receiver runtime information for a logical
        /// partition of an Event Hub. To enable the setting, refer to
        /// <see cref="EventProcessorOptions.EnableReceiverRuntimeMetric" />.
        /// </summary>
        public IEventHubReceiverRuntimeInformation RuntimeInformation
            => this.lazyReceiverRuntimneInfo.Value;

        #endregion

        #region Methods

        /// <summary>
        /// Writes the current offset and sequenceNumber to the checkpoint store
        /// via the checkpoint manager.
        /// </summary>
        /// <returns>The asynchronous task.</returns>
        public async Task CheckpointAsync()
        {
            await this.azurePartitionContext.CheckpointAsync();
        }

        /// <summary>
        /// Stores the offset and sequenceNumber from the provided received
        /// <see cref="IEventData" /> instance, then writes those values to the
        /// checkpoint store via the checkpoint manager.
        /// </summary>
        /// <param name="eventData">The <see cref="IEventData" /> instance.</param>
        /// <returns>The asynchronous task.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="IEventData"/> instance is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the the sequenceNumber from given <see cref="IEventData"/>
        /// instance is less than the last checkpointed value.
        /// </exception>
        public async Task CheckpointAsync(IEventData eventData)
        {
            Checks.Parameter(nameof(eventData), eventData)
                .NotNull();

            AzureEventData azureEventData = eventData.ToAzureEventData();

            await this.azurePartitionContext.CheckpointAsync(azureEventData);
        }

        #endregion
    }
}
