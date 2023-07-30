using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using TTMS.Internship.Services.TelemetryAPI.Handlers;
using TTMS.Internship.Services.TelemetryAPI.Models;
using TTMS.Internship.Services.TelemetryAPI.Services;

namespace TTMS.Internship.Services.TelemetryAPI.Tests
{
    public class TelemetryHandlerTests
    {
        [Fact]
        public void HandleTelemetryRequest_ValidRequest_ReturnsOkObjectResult()
        {
            var deviceID = "yourDeviceID";
            var startDate = "2023-07-01";
            var endDate = "2023-07-10";
            var telemetryEntityList = new List<TelemetryEntity>
        {
            new TelemetryEntity { DeviceID = deviceID, Timestamp = DateTime.Parse("2023-07-05") },
            new TelemetryEntity { DeviceID = deviceID, Timestamp = DateTime.Parse("2023-07-10") },
            new TelemetryEntity { DeviceID = deviceID, Timestamp = DateTime.Parse("2023-07-15") },
        };

            var serviceMock = new Mock<ITelemetryService>();

            serviceMock.Setup(s => s.GetTelemetry(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(new ResponseModel<TelemetryEntity> { HasError = false, Content = telemetryEntityList });

            var mapperMock = new Mock<IResponseMapper>();

            mapperMock.Setup(r => r.MapRequest(It.IsAny<ResponseModel<TelemetryEntity>>())).Returns(Results.Ok(telemetryEntityList));

            var telemetryHandler = new TelemetryHandler(serviceMock.Object, mapperMock.Object);

            var result = telemetryHandler.HandleTelemetryRequest(deviceID, startDate, endDate);

            result.Should().BeAssignableTo<Ok<List<TelemetryEntity>>>();
        }

        [Fact]
        public void HandleTelemetryRequest_ErrorResponse_ReturnsBadRequestObjectResult()
        {
            var deviceID = "yourDeviceID";
            var startDate = "2023-07-01";
            var endDate = "2023-07-10";
            var errorMessage = "Error message returned from the service";

            var serviceMock = new Mock<ITelemetryService>();

            serviceMock.Setup(s => s.GetTelemetry(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(new ResponseModel<TelemetryEntity> { HasError = true, ErrorMessage = errorMessage });

            var mapperMock = new Mock<IResponseMapper>();

            mapperMock.Setup(r => r.MapRequest(It.IsAny<ResponseModel<TelemetryEntity>>())).Returns(Results.BadRequest(errorMessage));

            var telemetryHandler = new TelemetryHandler(serviceMock.Object, mapperMock.Object);

            var result = telemetryHandler.HandleTelemetryRequest(deviceID, startDate, endDate);

            result.Should().BeAssignableTo<BadRequest<string>>();
        }
    }
}