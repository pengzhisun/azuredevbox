//------------------------------------------------------------------------------
// <copyright file="EventData.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using AzureEventData = Microsoft.Azure.EventHubs.EventData;

    /// <summary>
    /// Defines the event data class.
    /// </summary>
    internal sealed class EventData : IEventData
    {
        #region Fields

        /// <summary>
        /// The internal <see cref="AzureEventData" /> instance.
        /// </summary>
        private readonly AzureEventData azureEventData;

        /// <summary>
        /// The internal event data properties dictionary lazy instance.
        /// </summary>
        private readonly Lazy<IReadOnlyDictionary<string, object>> lazyProperties;

        /// <summary>
        /// The internal event data body bytes lazy instance.
        /// </summary>
        private readonly Lazy<IReadOnlyList<byte>> lazyBodyBytes;

        /// <summary>
        /// The internal event data body string lazy instance.
        /// </summary>
        private readonly Lazy<string> lazyBodyString;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventData"/> class.
        /// </summary>
        /// <param name="azureEventData">The <see cref="AzureEventData"/> instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the given <see cref="AzureEventData"/> instance is null.
        /// </exception>
        public EventData(AzureEventData azureEventData)
        {
            Checks.Parameter(nameof(azureEventData), azureEventData)
                .NotNull();

            this.azureEventData = azureEventData;

            this.lazyProperties =
                new Lazy<IReadOnlyDictionary<string, object>>(
                    () => this.azureEventData.Properties as IReadOnlyDictionary<string, object>);

            this.lazyBodyBytes =
                new Lazy<IReadOnlyList<byte>>(
                    () => azureEventData.Body.Array as IReadOnlyList<byte>);

            this.lazyBodyString =
                new Lazy<string>(
                    () => Encoding.UTF8.GetString(this.BodyBytes));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the event data body bytes.
        /// </summary>
        public byte[] BodyBytes => this.lazyBodyBytes.Value.ToArray();

        /// <summary>
        /// Gets the event data body string.
        /// </summary>
        public string BodyString => this.lazyBodyString.Value;

        /// <summary>
        /// Gets the event data body properties.
        /// </summary>
        public IReadOnlyDictionary<string, object> Properties
            => this.lazyProperties.Value;

        /// <summary>
        /// Gets the event data logical sequence number within the partition
        /// stream of the Event Hub.
        /// </summary>
        public long SequenceNumber
            => this.azureEventData.SystemProperties.SequenceNumber;

        /// <summary>
        /// Gets the event data actual enqueuing time in UTC.
        /// </summary>
        public DateTime EnqueuedTimeUtc
            => this.azureEventData.SystemProperties.EnqueuedTimeUtc;

        /// <summary>
        /// Gets the event data offset within the partition stream of the Event
        /// Hub.
        /// </summary>
        public string Offset => this.azureEventData.SystemProperties.Offset;

        /// <summary>
        /// Gets the event data partition key.
        /// </summary>
        public string PartitionKey
            => this.azureEventData.SystemProperties.TryGetValue(
                "x-opt-partition-key",
                out object partitionKey)
                    ? partitionKey.ToString()
                    : null;

        /// <summary>
        /// Gets the internal <see cref="AzureEventData" /> instance.
        /// </summary>
        internal AzureEventData AzureEventData => this.azureEventData;

        #endregion
    }
}