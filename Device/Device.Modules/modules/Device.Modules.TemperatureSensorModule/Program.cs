using Device.Modules.TemperatureSensorModule;

internal class Program
{
    private static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<TemperatureService>();
                services.AddScoped<ISensor, Sensor>();
            })
            .Build();

        host.Run();
    }
}