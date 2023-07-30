using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.FilteringMode;
using Iot.Device.Bmxx80.PowerMode;
using System.Device.I2c;
using UnitsNet;

namespace Device.Modules.TemperatureSensorModule
{
    public class Sensor : ISensor
    {
        private const int BusId = 1;

        private const int DeviceAddress = 0x76;

        private Bme280 sensor;

        public Sensor()
        {
            var i2cDevice = I2cDevice.Create(new I2cConnectionSettings(BusId, DeviceAddress));
            this.sensor = new Bme280(i2cDevice);

            this.sensor.SetPowerMode(Bmx280PowerMode.Normal);
            Thread.Sleep(1000);
            this.sensor.TemperatureSampling = Sampling.UltraHighResolution;
            this.sensor.PressureSampling = Sampling.UltraHighResolution;
            this.sensor.HumiditySampling = Sampling.Standard;
            this.sensor.FilterMode = Bmx280FilteringMode.X16;
        }

        public MessageBody ReadDataFromSensor()
        {
            if (!this.sensor.TryReadTemperature(out var temperature))
            {
                temperature = Temperature.Zero;
            }

            if (!this.sensor.TryReadPressure(out var pressure))
            {
                pressure = Pressure.Zero;
            }

            if (!this.sensor.TryReadHumidity(out var humidity))
            {
                humidity = RelativeHumidity.Zero;
            }

            return new MessageBody(temperature, pressure, humidity);
        }
    }
}
