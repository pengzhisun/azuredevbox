//------------------------------------------------------------------------------
// <copyright file="AzureCloseReasonExtensions.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    using AzureCloseReason = Microsoft.Azure.EventHubs.Processor.CloseReason;

    /// <summary>
    /// Defines the Azure EventHubs processor close reason enumeration extension
    /// methods.
    /// </summary>
    internal static class AzureCloseReasonExtensions
    {
        #region Methods

        /// <summary>
        /// Convert the Azure EventHubs processor close reason enumeration to
        /// <see cref="EventProcessorCloseReason" /> enumeration.
        /// </summary>
        /// <param name="azureCloseReason">the Azure EventHubs processor close reason enumeration value.</param>
        /// <returns>The <see cref="EventProcessorCloseReason" /> enumeration value.</returns>
        public static EventProcessorCloseReason ToEventProcessorCloseReason(
            this AzureCloseReason azureCloseReason)
        {
            switch (azureCloseReason)
            {
                case AzureCloseReason.LeaseLost:
                    return EventProcessorCloseReason.LeaseLost;
                case AzureCloseReason.Shutdown:
                    return EventProcessorCloseReason.Shutdown;
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }
}