using Azure.Messaging.EventHubs.Processor;

namespace TTMS.Internship.Services.TelemetryRead.Services.EventHandlers
{
    public interface IEventProcessorClientDecorator
    {
        event Func<ProcessEventArgs, Task> ProcessEventAsync;

        event Func<ProcessErrorEventArgs, Task> ProcessErrorAsync;

        Task StartProcessingAsync(CancellationToken token);
    }
}
