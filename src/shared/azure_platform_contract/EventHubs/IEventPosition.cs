namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    public interface IEventPosition
    {
         long? SequenceNumber { get; }
    }
}