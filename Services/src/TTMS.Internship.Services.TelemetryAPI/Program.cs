using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TTMS.Internship.Services.TelemetryAPI.Client;
using TTMS.Internship.Services.TelemetryAPI.Configuration;
using TTMS.Internship.Services.TelemetryAPI.Handlers;
using TTMS.Internship.Services.TelemetryAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        var azureKeyVaultUri = config.Get<AzureKeyVaultSetupConfig>() ?? throw new ArgumentNullException(nameof(config), "Configuration is required to retrieve AzureKeyVaultConfig");
        _ = azureKeyVaultUri.AzureKeyVaultUri ?? throw new ArgumentNullException(nameof(azureKeyVaultUri), "Configuration is required to retrieve AzureKeyVaultConfig");

        config = new ConfigurationBuilder()
            .AddConfiguration(config)
            .AddAzureKeyVault(new Uri(azureKeyVaultUri.AzureKeyVaultUri), new DefaultAzureCredential())
            .Build();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<ITelemetryService, TelemetryService>();
        builder.Services.AddTransient<ITelemetryHandler, TelemetryHandler>();
        builder.Services.AddTransient<IClientRepository, ClientRepository>();
        builder.Services.AddTransient<IResponseMapper, ResponseMapper>();
        builder.Services.AddSingleton(config.Get<AzureKeyVaultConfig>() ?? throw new ArgumentNullException(nameof(config), "Configuration is required to retrieve AzureKeyVaultConfige"));
        builder.Services.AddSingleton(config.Get<StorageConfig>() ?? throw new ArgumentNullException(nameof(config), "Configuration is required to retrieve StorageConfig"));
        builder.Services.AddSingleton<StorageClient>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/telemetry/{deviceID}", (ITelemetryService service, ITelemetryHandler telemetryHandler, string deviceID, [FromQuery] string startDate, [FromQuery] string endDate) =>
        {
            return telemetryHandler.HandleTelemetryRequest(deviceID, startDate, endDate);
        }).Produces<JsonContent>(200).Produces(400);

        app.Run();
    }
}