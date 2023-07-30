namespace Device.Modules.SimulationModule;

public class DataFactory
    {
        private const string DeviceID = "IOTEDGE_DEVICEID";
        private const double ExampleTemperature = 22;
        private const double ExamplePressure = 1013;
        private const double ExampleHumidity = 50;
        private const double ExampleCo2 = 600;

        public MessageBody CreateData()
        {
            var messageBody = new MessageBody
            {
                DeviceID = Environment.GetEnvironmentVariable(DeviceID),
                TimeCreated = DateTime.Now.ToString("F"),
                Temperature = DataCreation.CalculateData(ExampleTemperature),
                Pressure = DataCreation.CalculateData(ExamplePressure),
                Humidity = DataCreation.CalculateData(ExampleHumidity),
                Co2 = DataCreation.CalculateData(ExampleCo2),
            };

            return messageBody;
        }
    }