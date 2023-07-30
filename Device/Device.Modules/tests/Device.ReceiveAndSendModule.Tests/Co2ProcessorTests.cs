using Device.Modules.ReceiveAndSendModule.MessageOutput;
using Device.Modules.ReceiveAndSendModule.Wrapper;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Device.Modules.ReceiveAndSendModule.Tests
{
    public class Co2ProcessorTests
    {
        [Fact]
        public async Task ControlCO2DataMessageHandler_ValidJson_ProcessesDataAndSends()
        {
            // Arrange
            var dataHandler = new DataModels();
            var sendData = new Mock<ISendData>();
            var logger = new NullLogger<Co2Processor>();
            var co2Processor = new Co2Processor(dataHandler, sendData.Object, logger);

            var co2Body = new Co2Data()
            {
                Co2Value = 400,
            };
            var messageString = JsonConvert.SerializeObject(co2Body);
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            var message = new Message(messageBytes);

            var moduleClient = new Mock<IModuleClientWrapper>();
            moduleClient.Setup(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await co2Processor.ControlCO2DataMessageHandler(message, moduleClient.Object);

            // Assert
            Assert.Equal(MessageResponse.Completed, result);
            Assert.NotNull(dataHandler.Co2Data);
            Assert.Equal(400, dataHandler.Co2Data.Co2Value);
            sendData.Verify(s => s.ProcessData(moduleClient.Object), Times.Once);
        }

        [Fact]
        public async Task ControlCO2DataMessageHandler_InvalidJson_ReturnsCompleted()
        {
            // Arrange
            var dataHandler = new DataModels();
            var sendData = new Mock<ISendData>();
            var logger = new NullLogger<Co2Processor>();
            var co2Processor = new Co2Processor(dataHandler, sendData.Object, logger);

            var invalidJson = "Invalid JSON";
            var messageBytes = Encoding.UTF8.GetBytes(invalidJson);
            var message = new Message(messageBytes);

            var moduleClient = new Mock<IModuleClientWrapper>();
            moduleClient.Setup(m => m.SendEventAsync(It.IsAny<string>(), It.IsAny<Message>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await co2Processor.ControlCO2DataMessageHandler(message, moduleClient.Object);

            // Assert
            Assert.Equal(MessageResponse.Completed, result);
            Assert.Null(dataHandler.Co2Data);
            sendData.Verify(s => s.ProcessData(moduleClient.Object), Times.Never);
        }
    }
}