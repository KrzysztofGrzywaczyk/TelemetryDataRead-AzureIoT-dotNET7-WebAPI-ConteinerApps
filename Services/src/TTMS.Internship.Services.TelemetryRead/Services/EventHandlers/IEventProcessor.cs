using Azure.Messaging.EventHubs.Processor;

namespace TTMS.Internship.Services.TelemetryRead.Services.EventHandlers
{
    public interface IEventProcessor
    {
        public Task Process(ProcessEventArgs eventArgs);
    }
}
