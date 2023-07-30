using Device.Modules.ReceiveAndSendModule.MessageOutput;
using Device.Modules.ReceiveAndSendModule.Wrapper;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Device.Modules.ReceiveAndSendModule.Tests
{
    public class WeatherProcessorTests
    {
        [Fact]
        public async Task ControlWeatherDataMessageHandler_ValidJson_ProcessesDataAndSends()
        {
            // Arrange
            var dataHandler = new DataModels();
            var sendData = new Mock<ISendData>();
            var logger = new NullLogger<WeatherProcessor>();
            var weatherProcessor = new WeatherProcessor(dataHandler, sendData.Object, logger);

            var weatherData = new WeatherData()
            {
                Temperature = 25.5,
                Pressure = 1013.25,
                Humidity = 65,
            };
            var messageString = JsonConvert.SerializeObject(weatherData);
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            var message = new Message(messageBytes);

            var moduleClient = new Mock<IModuleClientWrapper>();
            moduleClient.Setup(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()));

            // Act
            var result = await weatherProcessor.ControlWeatherDataMessageHandler(message, moduleClient.Object);

            // Assert
            Assert.Equal(MessageResponse.Completed, result);
            Assert.NotNull(dataHandler.WeatherData);
            Assert.Equal(25.5, dataHandler.WeatherData.Temperature);
            Assert.Equal(1013.25, dataHandler.WeatherData.Pressure);
            Assert.Equal(65, dataHandler.WeatherData.Humidity);
            sendData.Verify(s => s.ProcessData(moduleClient.Object), Times.Once);
        }

        [Fact]
        public async Task ControlWeatherDataMessageHandler_InvalidJson_ReturnsCompleted()
        {
            // Arrange
            var dataHandler = new DataModels();
            var sendData = new Mock<ISendData>();
            var logger = new NullLogger<WeatherProcessor>();
            var weatherProcessor = new WeatherProcessor(dataHandler, sendData.Object, logger);

            var invalidJson = "Invalid JSON";
            var messageBytes = Encoding.UTF8.GetBytes(invalidJson);
            var message = new Message(messageBytes);

            var moduleClient = new Mock<IModuleClientWrapper>();
            moduleClient.Setup(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await weatherProcessor.ControlWeatherDataMessageHandler(message, moduleClient.Object);

            // Assert
            Assert.Equal(MessageResponse.Completed, result);
            Assert.Null(dataHandler.WeatherData);
            sendData.Verify(s => s.ProcessData(moduleClient.Object), Times.Never);
        }
    }
}