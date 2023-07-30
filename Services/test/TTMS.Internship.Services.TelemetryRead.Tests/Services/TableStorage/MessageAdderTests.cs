using Azure;
using Azure.Data.Tables;
using FluentAssertions;
using Moq;
using Service.TelemetryRead.Entities;
using TTMS.Internship.Services.TelemetryRead.Services.TableStorage;
using Xunit;

namespace TTMS.Internship.Services.TelemetryRead.Tests
{
    public class MessageAdderTests
    {
        private readonly Mock<ITableClientDecorator> mockTableClient;
        private readonly Mock<Response> mockResponse;

        public MessageAdderTests()
        {
            this.mockTableClient = new Mock<ITableClientDecorator>();
            this.mockResponse = new Mock<Response>();
        }

        [Fact]
        public async Task AddMessageAsync_ShouldAddMessage_WhenValidMessageProvided()
        {
            var service = new MessageAdder(this.mockTableClient.Object);
            var message = new MessageEntity { TimeCreated = DateTime.Now, DeviceID = "r", Temperature = 25, Pressure = 1000, Co2 = 0.04, Humidity = 30 };

            this.mockTableClient.Setup(client => client.AddEntityAsync(It.IsAny<TableEntity>())).ReturnsAsync(this.mockResponse.Object);

            var result = await service.AddMessageAsync(message);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(this.mockResponse.Object);
            this.mockTableClient.Verify(client => client.AddEntityAsync(It.IsAny<TableEntity>()), Times.Once);
        }
    }
}
