namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    using AzureCloseReason = Microsoft.Azure.EventHubs.Processor.CloseReason;

    public static class AzureCloseReasonExtensions
    {
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
    }
}