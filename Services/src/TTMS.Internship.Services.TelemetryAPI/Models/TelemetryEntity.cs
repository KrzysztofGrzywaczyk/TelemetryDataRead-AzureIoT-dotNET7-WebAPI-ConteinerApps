using Azure;
using Azure.Data.Tables;

namespace TTMS.Internship.Services.TelemetryAPI.Models
{
    public class TelemetryEntity : ITableEntity
    {
        public string? PartitionKey { get; set; }

        public string? RowKey { get; set; }

        public string DeviceID { get; set; } = string.Empty;

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Co2 { get; set; }

        public double Humidity { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
