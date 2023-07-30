using Device.Modules.Co2SensorModule;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using Newtonsoft.Json;
using System.Text;
using UnitsNet;

namespace Device.Modules.Co2SensorModule
{
    public class Co2Service : BackgroundService
    {
        private const string OutputMessageName = "simulatedData";
        private readonly ILogger<Co2Service> logger;
        private readonly ISensor sensor;
        private DesiredProperties? desiredProperties;
        private ModuleClient? ioTHubModuleClient;

        public Co2Service(ILogger<Co2Service> logger, ISensor sensor)
        {
            this.logger = logger;
            this.sensor = sensor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
            ITransportSettings[] settings = { mqttSetting };

            this.ioTHubModuleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);
            await this.ioTHubModuleClient.OpenAsync(stoppingToken);
            this.logger.LogInformation("IoT Hub module client initialized.");
            this.desiredProperties = new DesiredProperties();

            this.logger.LogInformation("Program starded...");
            this.logger.LogInformation("Creating ttyS0 port connection.... ");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (this.desiredProperties.SendData)
                    {
                        VolumeConcentration co2Concentration = this.sensor.GetConcentration();

                        var messageBody = new MessageBody(co2Concentration);
                        var messageString = JsonConvert.SerializeObject(messageBody);
                        var messageBytes = Encoding.UTF8.GetBytes(messageString);
                        var message = new Message(messageBytes);
                        await this.ioTHubModuleClient.SendEventAsync(OutputMessageName, message, stoppingToken);
                        this.logger.LogInformation("Sending message: {Message}", messageString);
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