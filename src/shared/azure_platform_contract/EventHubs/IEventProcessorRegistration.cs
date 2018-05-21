namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using System.Threading.Tasks;

    public interface IEventProcessorRegistration : IDisposable
    {
        IEventProcessor EventProcessor { get; }

        Task RegisterAsync(string eventHubConnectionString, string eventHubPath);

        Task UnregisterAsync();
    }
}