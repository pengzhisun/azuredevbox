namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using AzureEventPosition = Microsoft.Azure.EventHubs.EventPosition;

    internal sealed class EventPosition : IEventPosition
    {
        private readonly AzureEventPosition azureEventPosition;

        public EventPosition(AzureEventPosition azureEventPosition)
        {
            this.azureEventPosition = azureEventPosition;
        }

        public long? SequenceNumber => this.azureEventPosition.SequenceNumber;

        internal AzureEventPosition AzureEventPosition
            => this.azureEventPosition;
    }
}