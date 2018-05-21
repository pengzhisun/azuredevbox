namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System.Threading.Tasks;

    public delegate Task EventProcessorAsyncEventHandler(
        object sender,
        EventProcessorEventArgs args);
}