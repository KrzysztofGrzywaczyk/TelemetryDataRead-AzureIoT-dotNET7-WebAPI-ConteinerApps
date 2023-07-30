using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using TTMS.Internship.Services.TelemetryAPI.Handlers;
using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Tests
{
    public class ResponseMapperTests
    {
        [Fact]
        public void MapRequest_WithValidResponse_ReturnsOkResult()
        {
            var responseMapper = new ResponseMapper();
            var telemetryEntityList = new List<TelemetryEntity>
        {
            new TelemetryEntity { DeviceID = "deviceID", Timestamp = DateTime.Parse("2023-07-05") },
            new TelemetryEntity { DeviceID = "deviceID", Timestamp = DateTime.Parse("2023-07-10") },
            new TelemetryEntity { DeviceID = "deviceID", Timestamp = DateTime.Parse("2023-07-15") },
        };
            var responseModel = new ResponseModel<TelemetryEntity>
            {
                HasError = false,
                Content = telemetryEntityList,
            };

            var result = responseMapper.MapRequest(responseModel);

            result.Should().BeAssignableTo<Ok<List<TelemetryEntity>>>();
        }

        [Fact]
        public void MapRequest_WithErrorResponse_ReturnsBadRequestResult()
        {
            var responseMapper = new ResponseMapper();
            var errorMessage = "Invalid request";
            var responseModel = new ResponseModel<TelemetryEntity>
            {
                HasError = true,
                ErrorMessage = errorMessage,
            };

            var result = responseMapper.MapRequest(responseModel);

            result.Should().BeAssignableTo<BadRequest<string>>();
        }
    }
}