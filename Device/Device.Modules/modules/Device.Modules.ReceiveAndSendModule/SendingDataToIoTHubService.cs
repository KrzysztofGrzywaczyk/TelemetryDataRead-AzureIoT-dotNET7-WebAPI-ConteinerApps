using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Exceptions;
using Microsoft.Azure.Devices.Client.Transport.Mqtt;
using Device.Modules.ReceiveAndSendModule.Configuration;
using Device.Modules.ReceiveAndSendModule.Wrapper;

namespace Device.Modules.ReceiveAndSendModule
{
    public class SendingDataToIoTHubService : BackgroundService
    {
        private readonly DataConfig dataConfig;
        private readonly ILogger<SendingDataToIoTHubService> logger;
        private readonly WeatherProcessor weatherProcessor;
        private readonly Co2Processor co2Processor;
        private ModuleClientWrapper? clientWrapper;
        private ITransportSettings[] settings;

        public SendingDataToIoTHubService(ILogger<SendingDataToIoTHubService> logger, WeatherProcessor weatherHandler, Co2Processor co2Handler, DataConfig dataConfig)
        {
            MqttTransportSettings mqttTransport = new (TransportType.Mqtt_Tcp_Only);
            this.settings = new ITransportSettings[] { mqttTransport };
            this.weatherProcessor = weatherHandler;
            this.co2Processor = co2Handler;
            this.logger = logger;
            this.dataConfig = dataConfig;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await this.clientWrapper!.CloseAsync(stoppingToken);
            await base.StopAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (this.clientWrapper = new ModuleClientWrapper(this.settings))
                {
                    await this.clientWrapper.OpenAsync(stoppingToken);
                    this.logger.LogInformation("IoT Hub module client initialized.");

                    await this.clientWrapper.SetInputMessageHandlerAsync(this.dataConfig.InputTemperatureData, this.weatherProcessor.ControlWeatherDataMessageHandler, this.clientWrapper, stoppingToken);
                    await this.clientWrapper.SetInputMessageHandlerAsync(this.dataConfig.InputCO2Data, this.co2Processor.ControlCO2DataMessageHandler, this.clientWrapper, stoppingToken);

                    await Task.Delay(Timeout.Infinite, stoppingToken);
                }
            }
            catch (OperationCanceledException ex)
            {
                this.logger.LogError("The operation was canceled: {Message}", ex.Message);
            }
            catch (TimeoutException ex)
            {
                this.logger.LogError("An error occurred due to a timeout: {Message}", ex.Message);
            }
            catch (DeviceNotFoundException ex)
            {
                this.logger.LogError("The device specified was not found: {Message}", ex.Message);
            }
        }
    }
}