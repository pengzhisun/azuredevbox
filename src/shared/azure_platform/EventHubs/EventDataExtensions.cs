//------------------------------------------------------------------------------
// <copyright file="EventDataExtensions.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using AzureEventData = Microsoft.Azure.EventHubs.EventData;

    /// <summary>
    /// Defines the event data extension methods.
    /// </summary>
    internal static class EventDataExtensions
    {
        #region Methods

        /// <summary>
        /// Converts the <see cref="IEventData" /> instance to
        /// the <see cref="AzureEventData" /> instance.
        /// </summary>
        /// <param name="eventData">The <see cref="IEventData" /> instance.</param>
        /// <returns>The <see cref="AzureEventData" /> instance.</returns>
        public static AzureEventData ToAzureEventData(this IEventData eventData)
            => (eventData as EventData)?.AzureEventData;

        #endregion
    }
}