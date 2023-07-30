using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Client
{
    public interface IClientRepository
    {
        public List<TelemetryEntity> QueryForTelemetries(string deviceID, string startDate, string endDate);
    }
}
