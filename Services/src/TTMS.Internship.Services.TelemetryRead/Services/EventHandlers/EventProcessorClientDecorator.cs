using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using TTMS.Internship.Services.TelemetryRead.Configuration;

namespace TTMS.Internship.Services.TelemetryRead.Services.EventHandlers
{
    public class EventProcessorClientDecorator : IEventProcessorClientDecorator
    {
        private readonly EventProcessorClient client;

        public EventProcessorClientDecorator(TableStorageConfig tableStorageConfig, EventsConsumerConfig eventsConsumerConfig, AzureKeyVaultConfig azureKeyVaultConfig)
        {
            var storageClient = new BlobContainerClient(azureKeyVaultConfig.TableStorageConnectionString, tableStorageConfig.BlobContainerName);
            this.client = new EventProcessorClient(storageClient, EventHubConsumerClient.DefaultConsumerGroupName, azureKeyVaultConfig.HubNameSpaceConnectionString, eventsConsumerConfig.EventHubName);
        }

        public event Func<ProcessEventArgs, Task> ProcessEventAsync
        {
            add
            {
                this.client.ProcessEventAsync += value;
            }

            remove
            {
                this.client.ProcessEventAsync -= value;
            }
        }

        public event Func<ProcessErrorEventArgs, Task> ProcessErrorAsync
        {
            add
            {
                this.client.ProcessErrorAsync += value;
            }

            remove
            {
                this.client.ProcessErrorAsync -= value;
            }
        }

        public async Task StartProcessingAsync(CancellationToken token)
        {
            await this.client.StartProcessingAsync(token);
        }
    }
}
