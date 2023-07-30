using Azure;
using Azure.Data.Tables;
using TTMS.Internship.Services.TelemetryRead.Configuration;

namespace TTMS.Internship.Services.TelemetryRead.Services.TableStorage
{
    public class TableClientDecorator : ITableClientDecorator
    {
        private readonly TableClient tableClient;

        public TableClientDecorator(TableStorageConfig tableStorageConfig, AzureKeyVaultConfig azureKeyVaultConfig)
        {
            this.tableClient = new TableClient(azureKeyVaultConfig.TableStorageConnectionString, tableStorageConfig.TableStorageTableName);
        }

        public Task<Response> AddEntityAsync(TableEntity entity)
        {
            return this.tableClient.AddEntityAsync(entity);
        }
    }
}
