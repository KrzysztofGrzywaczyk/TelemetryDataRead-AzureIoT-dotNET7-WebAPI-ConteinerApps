using Device.Modules.SimulationModule;

namespace Device.SimulationModule.Tests
{
        public class DataCreationTests
        {
            [Theory]
            [InlineData(22.0)]
            [InlineData(1013.0)]
            [InlineData(55.5)]
            public void CalculateData_ForGivenTemperature_ShouldReturnChangedData(double currentData)
            {
                double result = DataCreation.CalculateData(currentData);

                Assert.NotEqual(currentData, result);
            }
        }
    }