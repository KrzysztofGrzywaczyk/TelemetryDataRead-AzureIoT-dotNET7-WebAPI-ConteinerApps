using Azure;
using Microsoft.AspNetCore.Mvc;
using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Handlers
{
    public class ResponseMapper : IResponseMapper
    {
        public IResult MapRequest(ResponseModel<TelemetryEntity> response)
        {
            var result = response.HasError ? Results.BadRequest(response.ErrorMessage) : Results.Ok(response.Content);

            return result;
        }
    }
}
