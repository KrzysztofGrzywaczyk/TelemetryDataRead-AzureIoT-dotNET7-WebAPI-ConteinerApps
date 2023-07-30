using TTMS.Internship.Services.TelemetryRead.Services.EventHandlers;

namespace TTMS.Internship.Services.TelemetryRead.Services.Events
{
    public class EventsConsumer : BackgroundService
    {
        private readonly IEventProcessorClientDecorator consumer;

        public EventsConsumer(IEventProcessor processEvent, IErrorProcessor processError, IEventProcessorClientDecorator eventProcessorClientDecorator)
        {
            this.consumer = eventProcessorClientDecorator;
            this.consumer.ProcessEventAsync += processEvent.Process;
            this.consumer.ProcessErrorAsync += processError.Process;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.consumer.StartProcessingAsync(stoppingToken);
        }
    }
}