using Device.Modules.ReceiveAndSendModule.Configuration;
using Device.Modules.ReceiveAndSendModule.Wrapper;

namespace Device.Modules.ReceiveAndSendModule
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<SendingDataToIoTHubService>();
                    services.AddSingleton(configuration.GetSection(ConfigurationConsts.DataConfig).Get<DataConfig>() ?? throw new ArgumentNullException(paramName: nameof(configuration), message: "Configuration is required to retrieve DataConfig."));
                    services.AddSingleton<DataModels>();
                    services.AddSingleton<WeatherProcessor>();
                    services.AddSingleton<Co2Processor>();
                    services.AddSingleton<ISendData, SendData>();
                })
                .Build();

            host.Run();
        }
    }
}
