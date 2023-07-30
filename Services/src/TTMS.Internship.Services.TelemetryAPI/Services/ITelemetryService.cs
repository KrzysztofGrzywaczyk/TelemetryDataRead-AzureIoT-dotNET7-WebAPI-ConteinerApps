using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Services
{
    public interface ITelemetryService
    {
        public ResponseModel<TelemetryEntity> GetTelemetry(string deviceID, string startDate, string endDate);
    }
}
