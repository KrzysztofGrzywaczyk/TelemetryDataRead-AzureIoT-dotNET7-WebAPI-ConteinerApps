using Azure.Data.Tables;
using TTMS.Internship.Services.TelemetryAPI.Configuration;

namespace TTMS.Internship.Services.TelemetryAPI.Client
{
    public class StorageClient
    {
        private readonly StorageConfig config;
        private readonly AzureKeyVaultConfig keyVaultConfig;

        public StorageClient(StorageConfig config, AzureKeyVaultConfig keyVaultConfig)
        {
            this.config = config;
            this.keyVaultConfig = keyVaultConfig;
        }

        public TableClient CreateClient()
        {
            var serviceClient = new TableServiceClient(this.keyVaultConfig.StorageConnectionString);
            var tableClient = serviceClient.GetTableClient(this.config.StorageName);
            return tableClient;
        }
    }
}
