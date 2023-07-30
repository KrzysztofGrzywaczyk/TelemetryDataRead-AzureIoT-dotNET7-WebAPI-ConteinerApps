using Microsoft.AspNetCore.Mvc;

namespace TTMS.Internship.Services.TelemetryAPI.Handlers
{
    public interface ITelemetryHandler
    {
        IResult HandleTelemetryRequest(string deviceID, string startDate, string endDate);
    }
}
