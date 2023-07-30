using Azure.Identity;
using TTMS.Internship.Services.TelemetryRead.Configuration;
using TTMS.Internship.Services.TelemetryRead.Services.EventHandlers;
using TTMS.Internship.Services.TelemetryRead.Services.Events;
using TTMS.Internship.Services.TelemetryRead.Services.TableStorage;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var azureKeyVaultUrl = configuration.GetSection(nameof(AzureKeyVaultConfig)).Get<AzureKeyVaultSetupConfig>() ?? throw new ArgumentNullException(nameof(configuration), "Configuration is required to retrieve ConfigurationConfig.");

_ = azureKeyVaultUrl.AzureKeyVaultUrl ?? throw new ArgumentNullException(nameof(configuration), "Configuration is required to retrieve AzureKeyVaultUrl.");

configuration = new ConfigurationBuilder()
    .AddConfiguration(configuration)
    .AddAzureKeyVault(new Uri(azureKeyVaultUrl.AzureKeyVaultUrl), new DefaultAzureCredential())
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<EventsConsumer>();
        services.AddSingleton(configuration.GetSection(nameof(EventsConsumerConfig)).Get<EventsConsumerConfig>() ?? throw new ArgumentNullException(nameof(configuration), "Configuration is required to retrieve EventsConsumerConfig."));
        services.AddSingleton(configuration.GetSection(nameof(TableStorageConfig)).Get<TableStorageConfig>() ?? throw new ArgumentNullException(nameof(configuration), "Configuration is required to retrieve TableStorageConfig."));
        services.AddSingleton(configuration.Get<AzureKeyVaultConfig>() ?? throw new ArgumentNullException(nameof(configuration), "Configuration is required to retrieve AzureKeyVaultConfig."));
        services.AddSingleton<ITableClientDecorator, TableClientDecorator>();
        services.AddSingleton<IEventProcessorClientDecorator, EventProcessorClientDecorator>();
        services.AddTransient<MessageAdder>();
        services.AddSingleton<IEventProcessor, EventProcessor>();
        services.AddSingleton<IErrorProcessor, ErrorProcessor>();
    })
    .Build();

host.Run();