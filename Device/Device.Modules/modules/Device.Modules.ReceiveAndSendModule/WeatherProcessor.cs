using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using Device.Modules.ReceiveAndSendModule.MessageOutput;
using Device.Modules.ReceiveAndSendModule.Wrapper;

namespace Device.Modules.ReceiveAndSendModule
{
    public class WeatherProcessor
    {
        private readonly DataModels dataHandler;
        private readonly ISendData sendData;
        private readonly ILogger logger;

        public WeatherProcessor(DataModels dataHandler, ISendData sendData, ILogger<WeatherProcessor> logger)
        {
            this.dataHandler = dataHandler;
            this.sendData = sendData;
            this.logger = logger;
            this.dataHandler.WeatherData = null;
        }

        public async Task<MessageResponse> ControlWeatherDataMessageHandler(Message message, object userContext)
        {
            var moduleClient = userContext as IModuleClientWrapper;
            if (moduleClient == null)
            {
                this.logger.LogError("{UserContext} doesn't contain expected value", nameof(userContext));
                throw new InvalidOperationException($"{nameof(userContext)} doesn't contain expected value");
            }

            var messageBytes = message.GetBytes();
            var messageString = Encoding.UTF8.GetString(messageBytes);
            this.logger.LogInformation("Received message: [{MessageString}]", messageString);

            if (string.IsNullOrEmpty(messageString))
            {
                return MessageResponse.Completed;
            }

            try
            {
                this.dataHandler.WeatherData = JsonConvert.DeserializeObject<WeatherData>(messageString);
                await this.sendData.ProcessData(moduleClient);
            }
            catch (JsonException)
            {
                this.logger.LogError("Failed to deserialize weather data. Invalid JSON: {MessageString}", messageString);
                return MessageResponse.Completed;
            }

            return MessageResponse.Completed;
        }
    }
}