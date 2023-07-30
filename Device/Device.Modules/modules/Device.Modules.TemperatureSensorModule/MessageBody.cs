namespace Device.Modules.TemperatureSensorModule
{
    public class MessageBody
    {
        public MessageBody(UnitsNet.Temperature temperature, UnitsNet.Pressure pressure, UnitsNet.RelativeHumidity humidity)
        {
            this.Temperature = Math.Round(temperature.DegreesCelsius, 2);
            this.Pressure = Math.Round(pressure.Hectopascals, 2);
            this.Humidity = Math.Round(humidity.Percent, 2);
        }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Humidity { get; set; }
    }
}
