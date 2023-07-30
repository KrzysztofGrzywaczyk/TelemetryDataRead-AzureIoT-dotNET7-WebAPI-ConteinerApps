using TTMS.Internship.Services.TelemetryAPI.Services;

namespace TTMS.Internship.Services.TelemetryAPI.Handlers
{
    public class TelemetryHandler : ITelemetryHandler
    {
        private readonly ITelemetryService service;
        private readonly IResponseMapper responseMapper;

        public TelemetryHandler(ITelemetryService service, IResponseMapper responseMapper)
        {
            this.service = service;
            this.responseMapper = responseMapper;
        }

        public IResult HandleTelemetryRequest(string deviceID, string startDate, string endDate)
        {
            var response = this.service.GetTelemetry(deviceID, startDate, endDate);
            return this.responseMapper.MapRequest(response);
        }
    }
}
