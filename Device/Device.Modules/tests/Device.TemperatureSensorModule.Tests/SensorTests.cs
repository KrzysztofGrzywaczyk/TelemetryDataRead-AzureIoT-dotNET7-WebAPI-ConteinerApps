using Device.Modules.TemperatureSensorModule;
using Moq;
using UnitsNet;

namespace Device.Tests.TemperatureSensorModule
{
    public class SensorTests
    {
        [Theory]
        [InlineData(25, 101325, 50)]
        [InlineData(30, 102000, 60)]
        [InlineData(18, 101001, 45)]
        public void ReadDataFromSensor_ReturnsValidData(
            double temperatureValue,
            double pressureValue,
            double humidityValue)
        {
            var sensorMock = new Mock<ISensor>();

            sensorMock.Setup(s => s.ReadDataFromSensor())
                .Returns(new MessageBody(
                    Temperature.FromDegreesCelsius(temperatureValue),
                    Pressure.FromPascals(pressureValue),
                    RelativeHumidity.FromPercent(humidityValue)));

            var sensor = sensorMock.Object;

            MessageBody data = sensor.ReadDataFromSensor();

            Assert.NotEqual(Temperature.Zero.Value, data.Temperature);
            Assert.NotEqual(Pressure.Zero.Value, data.Pressure);
            Assert.NotEqual(RelativeHumidity.Zero.Value, data.Humidity);
        }
    }
}
