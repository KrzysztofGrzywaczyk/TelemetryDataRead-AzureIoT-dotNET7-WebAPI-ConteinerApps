using Azure;
using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Client
{
    public class ClientRepository : IClientRepository
    {
        private const string AnyDeviceAlias = "Any";

        private readonly StorageClient storageClient;

        public ClientRepository(StorageClient storageClient)
        {
            this.storageClient = storageClient;
        }

        public List<TelemetryEntity> QueryForTelemetries(string deviceID, string startDate, string endDate)
        {
            var startDateParsed = DateTime.Parse(startDate);
            var endDateParsed = DateTime.Parse(endDate);
            var tableClient = this.storageClient.CreateClient();

            Pageable<TelemetryEntity> oDataQueryEntities = deviceID.Equals(AnyDeviceAlias) ?
                tableClient.Query<TelemetryEntity>(filter: x => x.Timestamp >= startDateParsed && x.Timestamp <= endDateParsed) :
                tableClient.Query<TelemetryEntity>(filter: x => x.Timestamp >= startDateParsed && x.Timestamp <= endDateParsed && x.DeviceID.Equals(deviceID));

            var entities = oDataQueryEntities.ToList();

            return entities;
        }
    }
}
