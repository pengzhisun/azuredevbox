namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using AzureEventData = Microsoft.Azure.EventHubs.EventData;

    public static class EventDataExtensions
    {
        public static AzureEventData ToAzureEventData(this IEventData eventData)
            => (eventData as EventData)?.AzureEventData;
    }
}