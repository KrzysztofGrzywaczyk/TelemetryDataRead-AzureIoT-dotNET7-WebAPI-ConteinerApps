using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using Newtonsoft.Json;
using System.Text;

namespace Device.Modules.SimulationModule
{
    public class SimulationService : BackgroundService
    {
        private const string OutputMessageName = "sensorsOutput";

        private readonly ILogger<SimulationService> logger;

        private DesiredProperties desiredProperties;

        private DataFactory dataFactory;

        private int counter;

        public SimulationService(ILogger<SimulationService> logger)
        {
            this.logger = logger;
            this.desiredProperties = new DesiredProperties();
            this.dataFactory = new DataFactory();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
            ITransportSettings[] settings = { mqttSetting };

            var ioTHubModuleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);
            await ioTHubModuleClient.OpenAsync();
            this.logger.LogInformation("IoT Hub module client initialized.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (this.desiredProperties.SendData)
                    {
                        this.counter++;
                        var messageBody = this.dataFactory.CreateData();

                        var messageString = JsonConvert.SerializeObject(messageBody);
                        var messageBytes = Encoding.UTF8.GetBytes(messageString);
                        var message = new Message(messageBytes);
                        await ioTHubModuleClient.SendEventAsync(OutputMessageName, message);
                        this.logger.LogInformation("{Date} {Time}, Sending message: {Counter}, Body: {Message}", DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString(), this.counter, messageString);
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(this.desiredProperties.SendInterval), stoppingToken);
                }
                catch (Exception ex)
                {
                    this.logger.LogInformation("[ERROR] Unexpected Exception {message}", ex.Message);
                    this.logger.LogInformation(ex.ToString());
                }
            }
        }
    }
}