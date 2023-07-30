using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using Device.Modules.ReceiveAndSendModule.Configuration;
using Device.Modules.ReceiveAndSendModule.MessageOutput.MessageBodyBuilder;
using Device.Modules.ReceiveAndSendModule.Wrapper;

namespace Device.Modules.ReceiveAndSendModule
{
    public class SendData : ISendData
    {
        private const string DeviceID = "IOTEDGE_DEVICEID";

        private readonly DataConfig dataConfig;
        private readonly DataModels dataHandler;
        private readonly ILogger logger;

        public SendData(DataModels dataHandler, ILogger<SendData> logger, DataConfig dataConfig)
        {
            this.dataHandler = dataHandler;
            this.logger = logger;
            this.dataConfig = dataConfig;
        }

        public async Task ProcessData(IModuleClientWrapper client)
        {
            if (this.dataHandler.WeatherData != null && this.dataHandler.Co2Data != null)
            {
                var messageBody = new MessageBodyBuilder()
                    .SetDeviceId(Environment.GetEnvironmentVariable(DeviceID))
                    .SetTemperature(this.dataHandler.WeatherData.Temperature)
                    .SetPressure(this.dataHandler.WeatherData.Pressure)
                    .SetCo2Value(this.dataHandler.Co2Data.Co2Value)
                    .SetHumidity(this.dataHandler.WeatherData.Humidity)
                    .SetTimeCreated(DateTime.Now)
                    .Build();

                var messageString = JsonConvert.SerializeObject(messageBody);
                var messageBytes = Encoding.UTF8.GetBytes(messageString);
                this.logger.LogInformation("Body: [{MessageString}]", messageString);
                var messageToSend = new Message(messageBytes);

                await client.SendEventAsync(this.dataConfig.OutputData, messageToSend);
                this.logger.LogInformation("Received messages sent");

                this.dataHandler.WeatherData = null;
                this.dataHandler.Co2Data = null;
            }
        }
    }
}
