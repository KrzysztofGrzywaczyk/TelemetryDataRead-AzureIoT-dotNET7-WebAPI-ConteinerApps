using System.Text;
using Azure;
using Azure.Messaging.EventHubs.Processor;
using Newtonsoft.Json;
using Service.TelemetryRead.Entities;
using TTMS.Internship.Services.TelemetryRead.Services.TableStorage;

namespace TTMS.Internship.Services.TelemetryRead.Services.EventHandlers
{
    public class EventProcessor : IEventProcessor
    {
        private readonly ILogger<EventProcessor> logger;

        private readonly MessageAdder messageAdder;

        public EventProcessor(ILogger<EventProcessor> logger, MessageAdder messageAdder)
        {
            this.logger = logger;
            this.messageAdder = messageAdder;
        }

        public async Task Process(ProcessEventArgs eventArgs)
        {
            var data = Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray());
            try
            {
                var messageBody = JsonConvert.DeserializeObject<MessageEntity>(data) ?? throw new NullReferenceException();
                var jsonMessage = JsonConvert.SerializeObject(messageBody, Formatting.Indented);
                this.logger.LogInformation("Received message: {Message}", jsonMessage);
                var response = await this.messageAdder.AddMessageAsync(messageBody);
                this.logger.LogInformation("Table storage response: {Response}", response.ToString());
            }
            catch (NullReferenceException ex)
            {
                this.logger.LogError(ex, "Deserialization to MessageEntity failed. Received data is null.");
            }
            catch (JsonException ex)
            {
                this.logger.LogError(ex, "Deserialization to MessageEntity failed. Received data is in incorrect format.");
            }
            catch (FormatException ex)
            {
                this.logger.LogError(ex, "Adding message to table storage failed. Message data format is incorrect.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                this.logger.LogError(ex, "Adding message to table storage failed. Message data argument out of range.");
            }
            catch (RequestFailedException ex)
            {
                this.logger.LogError(ex, "Adding message to table storage failed. Request to table storage failed.");
            }

            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }
    }
}
