//------------------------------------------------------------------------------
// <copyright file="EventProcessorOptionsExtensions.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using AzureEventPosition = Microsoft.Azure.EventHubs.EventPosition;
    using AzureEventProcessorOptions = Microsoft.Azure.EventHubs.Processor.EventProcessorOptions;

    internal static class EventProcessorOptionsExtensions
    {
        private static readonly AzureEventProcessorOptions DefaultAzureOptions =
            AzureEventProcessorOptions.DefaultOptions;

        public static AzureEventProcessorOptions ToAzureOptions(
            this IEventProcessorOptions options)
        {
            if (options == null)
            {
                return DefaultAzureOptions;
            }

            AzureEventProcessorOptions azureOptions =
                new AzureEventProcessorOptions
                {
                    MaxBatchSize =
                        options.MaxBatchSize
                            ?? DefaultAzureOptions.MaxBatchSize,
                    ReceiveTimeout =
                        options.ReceiveTimeout
                            ?? DefaultAzureOptions.ReceiveTimeout,
                    EnableReceiverRuntimeMetric =
                        options.EnableReceiverRuntimeMetric
                            ?? DefaultAzureOptions.EnableReceiverRuntimeMetric,
                    PrefetchCount =
                        options.PrefetchCount
                            ?? DefaultAzureOptions.PrefetchCount,
                    InvokeProcessorAfterReceiveTimeout =
                        options.InvokeProcessorAfterReceiveTimeout
                            ?? DefaultAzureOptions.InvokeProcessorAfterReceiveTimeout,
                };

            if (options.InitialOffsetProvider == null)
            {
                azureOptions.InitialOffsetProvider =
                    DefaultAzureOptions.InitialOffsetProvider;
            }
            else
            {
                azureOptions.InitialOffsetProvider =
                    (partitionId) =>
                        (options.InitialOffsetProvider.Invoke(partitionId)
                            as EventPosition)?.AzureEventPosition;
            }

            if (options is EventProcessorOptions internalOptions)
            {
                azureOptions.SetExceptionHandler(
                    args => internalOptions.ExceptionHandler(
                        new ExceptionReceivedEventArgs(args)));
            }

            return azureOptions;
        }
    }
}