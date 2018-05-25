namespace Winl.AzureDevBox.AzurePlatform.EventHubs
{
    using System;
    using AzureEventPosition = Microsoft.Azure.EventHubs.EventPosition;

    internal sealed class EventPositionFactory : IEventPositionFactory
    {
        public IEventPosition FromStart()
            => GetEventPosition(() => AzureEventPosition.FromStart());

        public IEventPosition FromEnd()
            => GetEventPosition(() => AzureEventPosition.FromEnd());

        public IEventPosition FromEnqueuedTime(DateTime enqueuedTimeUtc)
            => GetEventPosition(
                () => AzureEventPosition.FromEnqueuedTime(enqueuedTimeUtc));

        public IEventPosition FromOffset(string offset, bool inclusive = false)
            => GetEventPosition(
                () => AzureEventPosition.FromOffset(offset, inclusive));

        public IEventPosition FromSequenceNumber(
            long sequenceNumber,
            bool inclusive = false)
            => GetEventPosition(
                () => AzureEventPosition.FromSequenceNumber(
                    sequenceNumber,
                    inclusive));

        private static IEventPosition GetEventPosition(
            Func<AzureEventPosition> getFunc)
        {
            AzureEventPosition azureEventPosition = getFunc();

            return new EventPosition(azureEventPosition);
        }
    }
}