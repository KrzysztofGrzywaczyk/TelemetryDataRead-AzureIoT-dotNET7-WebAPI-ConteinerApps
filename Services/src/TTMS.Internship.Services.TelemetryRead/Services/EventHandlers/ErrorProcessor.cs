using Azure.Messaging.EventHubs.Processor;

namespace TTMS.Internship.Services.TelemetryRead.Services.EventHandlers
{
    public class ErrorProcessor : IErrorProcessor
    {
        private readonly ILogger<ErrorProcessor> logger;

        public ErrorProcessor(ILogger<ErrorProcessor> logger)
        {
            this.logger = logger;
        }

        public Task Process(ProcessErrorEventArgs errorArgs)
        {
            this.logger.LogError(errorArgs.Exception, "An error occurred in the event processor.");
            return Task.CompletedTask;
        }
    }
}
