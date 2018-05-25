namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;

    public interface IEventPositionFactory
    {
        IEventPosition FromStart();

        IEventPosition FromEnd();

        IEventPosition FromEnqueuedTime(DateTime enqueuedTimeUtc);

        IEventPosition FromOffset(string offset, bool inclusive = false);

        IEventPosition FromSequenceNumber(
            long sequenceNumber,
            bool inclusive = false);
    }
}