using Azure.Messaging.EventHubs.Processor;

namespace TTMS.Internship.Services.TelemetryRead.Services.EventHandlers
{
    public interface IErrorProcessor
    {
        public Task Process(ProcessErrorEventArgs errorArgs);
    }
}
