using Device.Modules.SimulationModule;

internal class Program
{
    private static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<SimulationService>();
            })
            .Build();

        host.Run();
    }
}