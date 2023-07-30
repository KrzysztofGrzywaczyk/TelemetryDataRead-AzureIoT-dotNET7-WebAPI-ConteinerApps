using Device.Modules.ReceiveAndSendModule.Configuration;
using Device.Modules.ReceiveAndSendModule.MessageOutput;
using Device.Modules.ReceiveAndSendModule.Wrapper;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Device.Modules.ReceiveAndSendModule.Tests
{
    public class SendDataTests
    {
        [Fact]
        public async Task ProcessData_ValidData_SendsEvent()
        {
            // Arrange
            var dataHandler = new DataModels();
            dataHandler.WeatherData = new WeatherData
            {
                Temperature = 25.5,
                Pressure = 1013.25,
                Humidity = 65,
            };
            dataHandler.Co2Data = new Co2Data
            {
                Co2Value = 400,
            };

            var sendData = new SendData(dataHandler, new NullLogger<SendData>(), new DataConfig());
            var messageToSend = default(Message);
            var moduleClient = new Mock<IModuleClientWrapper>();
            moduleClient.Setup(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()))
                .Callback<string, Message>((output, message) => messageToSend = message)
                .Returns(Task.CompletedTask);

            // Act
            await sendData.ProcessData(moduleClient.Object);

            // Assert
            Assert.NotNull(messageToSend);
            var messageString = Encoding.UTF8.GetString(messageToSend.GetBytes());
            var messageBody = JsonConvert.DeserializeObject<MessageBody>(messageString);
            Assert.NotNull(messageBody);
            Assert.Equal(25.5, messageBody.Temperature);
            Assert.Equal(1013.25, messageBody.Pressure);
            Assert.Equal(65, messageBody.Humidity);
            Assert.Equal(400, messageBody.Co2);
        }

        [Fact]
        public async Task ProcessData_NullData_DoesNotSendEvent()
        {
            // Arrange
            var dataHandler = new DataModels();
            var sendData = new SendData(dataHandler, new NullLogger<SendData>(), new DataConfig());
            var moduleClient = new Mock<IModuleClientWrapper>();
            moduleClient.Setup(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()))
                .Returns(Task.CompletedTask);

            // Act
            await sendData.ProcessData(moduleClient.Object);

            // Assert
            moduleClient.Verify(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()), Times.Never);
        }
    }
}