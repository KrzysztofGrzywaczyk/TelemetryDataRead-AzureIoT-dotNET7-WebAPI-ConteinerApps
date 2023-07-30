using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using Newtonsoft.Json;
using System.Text;

namespace Device.Modules.TemperatureSensorModule
{
    public class TemperatureService : BackgroundService
    {
        private const string OutputMessageName = "sensorsOutput";

        private readonly ILogger<TemperatureService> logger;

        private readonly ISensor sensor;

        private readonly DesiredProperties desiredProperties;

        public TemperatureService(ILogger<TemperatureService> logger, ISensor sensor)
        {
            this.desiredProperties = new DesiredProperties();
            this.logger = logger;
            this.sensor = sensor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
            ITransportSettings[] settings = { mqttSetting };

            var ioTHubModuleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);
            await ioTHubModuleClient.OpenAsync(stoppingToken);
            this.logger.LogInformation("IoT Hub module client initialized.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (this.desiredProperties.SendData)
                    {
                        var messageBody = this.sensor.ReadDataFromSensor();
                        this.logger.LogInformation("Temperature: {Temperature}, Pressure: {Pressure}, Humidity: {Humidity}", messageBody.Temperature, messageBody.Pressure, messageBody.Humidity);
                        var messageString = JsonConvert.SerializeObject(messageBody);
                        var messageBytes = Encoding.UTF8.GetBytes(messageString);
                        var message = new Message(messageBytes);
                        await ioTHubModuleClient.SendEventAsync(OutputMessageName, message, stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(this.desiredProperties.SendInterval), stoppingToken);
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "An unexpected error occured");
                }
            }
        }
    }
}