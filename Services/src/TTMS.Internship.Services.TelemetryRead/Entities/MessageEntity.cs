namespace Service.TelemetryRead.Entities
{
    public class MessageEntity
    {
        public string? DeviceID { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Co2 { get; set; }

        public double Humidity { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}