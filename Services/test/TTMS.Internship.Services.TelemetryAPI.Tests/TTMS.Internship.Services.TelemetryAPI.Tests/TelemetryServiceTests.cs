using Azure;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TTMS.Internship.Services.TelemetryAPI.Client;
using TTMS.Internship.Services.TelemetryAPI.Models;
using TTMS.Internship.Services.TelemetryAPI.Services;

namespace TTMS.Internship.Services.TelemetryAPI.Tests
{
    public class TelemetryServiceTests
    {
        [Fact]
        public void GetTelemetry_WithValidData_ReturnsCorrectResponse()
        {
            string deviceID = "Simulator";
            string startDate = "2023-july-01";
            string endDate = "2023-07-20";
            var telemetryEntityList = new List<TelemetryEntity>
        {
            new TelemetryEntity { DeviceID = deviceID, Timestamp = DateTime.Parse("2023-07-05") },
            new TelemetryEntity { DeviceID = deviceID, Timestamp = DateTime.Parse("2023-07-10") },
            new TelemetryEntity { DeviceID = deviceID, Timestamp = DateTime.Parse("2023-07-15") },
        };

            var loggerMock = new Mock<ILogger<TelemetryService>>();
            var clientRepositoryMock = new Mock<IClientRepository>();

            clientRepositoryMock.Setup(repo => repo.QueryForTelemetries(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(telemetryEntityList);

            var telemetryService = new TelemetryService(loggerMock.Object, clientRepositoryMock.Object);

            var response = telemetryService.GetTelemetry(deviceID, startDate, endDate);

            response.HasError.Should().BeFalse();
            response.Content.Should().BeEquivalentTo(telemetryEntityList);
        }
    }
}