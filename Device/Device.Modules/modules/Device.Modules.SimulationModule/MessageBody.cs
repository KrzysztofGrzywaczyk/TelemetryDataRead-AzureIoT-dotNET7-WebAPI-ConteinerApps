using Newtonsoft.Json;

namespace Device.Modules.SimulationModule;

public class MessageBody
    {
        public string? DeviceID { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Co2 { get; set; }

        public double Humidity { get; set; }

        public string? TimeCreated { get; set; }
    }
