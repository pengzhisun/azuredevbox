namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    public interface IExceptionReceivedEventArgs
    {
        string Hostname { get; }

        string PartitionId { get; }

        Exception Exception { get; }

        string Action { get; }
    }
}