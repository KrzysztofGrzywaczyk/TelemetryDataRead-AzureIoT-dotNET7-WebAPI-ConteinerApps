using Azure;
using Azure.Data.Tables;
using Service.TelemetryRead.Entities;
using TTMS.Internship.Services.TelemetryRead.Extensions;

namespace TTMS.Internship.Services.TelemetryRead.Services.TableStorage
{
    public class MessageAdder
    {
        private readonly ITableClientDecorator tableClient;

        public MessageAdder(ITableClientDecorator tableClient)
        {
            this.tableClient = tableClient;
        }

        public async Task<Response> AddMessageAsync(MessageEntity message)
        {
            var partitionKey = message.PartitionKeyGenerate();
            var rowKey = message.RowKeyGenerate();

            var tableEntity = new TableEntity(partitionKey, rowKey)
                {
                    { nameof(message.DeviceID), message.DeviceID },
                    { nameof(message.Temperature), message.Temperature },
                    { nameof(message.Pressure), message.Pressure },
                    { nameof(message.Co2), message.Co2 },
                    { nameof(message.Humidity), message.Humidity },
                    { nameof(message.TimeCreated), message.TimeCreated.ToUniversalTime().ToString("o") },
                };

            return await this.tableClient.AddEntityAsync(tableEntity);
        }
    }
}
