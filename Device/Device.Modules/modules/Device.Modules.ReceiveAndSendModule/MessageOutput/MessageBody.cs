namespace Device.Modules.ReceiveAndSendModule.MessageOutput
{
    public class MessageBody
    {
        public string? DeviceID { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double Pressure { get; set; }

        public double Co2 { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}
