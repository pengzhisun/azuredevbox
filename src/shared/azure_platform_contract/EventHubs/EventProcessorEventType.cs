namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    public enum EventProcessorEventType
    {
        Unknown,
        Closed,
        Opened,
        ErrorOccurred,
        DataReceived
    }
}