namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using AzureExceptionReceivedEventArgs = Microsoft.Azure.EventHubs.Processor.ExceptionReceivedEventArgs;

    public sealed class ExceptionReceivedEventArgs : IExceptionReceivedEventArgs
    {
        private readonly AzureExceptionReceivedEventArgs azureEventArgs;

        public ExceptionReceivedEventArgs(
            AzureExceptionReceivedEventArgs azureEventArgs)
        {
            this.azureEventArgs = azureEventArgs;
        }

        public string Hostname => this.azureEventArgs.Hostname;

        public string PartitionId => this.azureEventArgs.PartitionId;

        public Exception Exception => this.azureEventArgs.Exception;

        public string Action => this.azureEventArgs.Action;
    }
}