using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Handlers
{
    public interface IResponseMapper
    {
        public IResult MapRequest(ResponseModel<TelemetryEntity> response);
    }
}
