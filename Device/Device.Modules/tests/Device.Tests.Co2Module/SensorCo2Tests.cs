using Device.Modules.Co2SensorModule;
using Moq;
using UnitsNet;

namespace Device.Tests.Co2Module
{
    public class SensorCo2Tests
    {
        [Fact]
        public void GetConcentration_ReturnsCorrectValueAndUnit()
        {
            double concentrationValue = 450.0;
            var sensorCo2Mock = new Mock<ISensor>();
            sensorCo2Mock.Setup(x => x.GetConcentration()).Returns(VolumeConcentration.FromPartsPerMillion(concentrationValue));
            var sensor = sensorCo2Mock.Object;

            VolumeConcentration expectedConcentration = VolumeConcentration.FromPartsPerMillion(concentrationValue);
            VolumeConcentration actualConcentration = sensor.GetConcentration();

            Assert.Equal(expectedConcentration, actualConcentration);
        }

        [Fact]
        public void GetConcentration_NotReturnesZeroValue()
        {
            double concentrationValue = 450.0;
            var sensorCo2Mock = new Mock<ISensor>();
            sensorCo2Mock.Setup(x => x.GetConcentration()).Returns(VolumeConcentration.FromPartsPerMillion(concentrationValue));
            var sensor = sensorCo2Mock.Object;

            VolumeConcentration actualConcentration = sensor.GetConcentration();

            Assert.NotEqual(VolumeConcentration.Zero, actualConcentration);
        }
    }
}