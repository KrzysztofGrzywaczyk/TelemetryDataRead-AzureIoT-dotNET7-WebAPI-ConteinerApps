using Azure;
using Azure.Data.Tables;
using TTMS.Internship.Services.TelemetryAPI.Client;
using TTMS.Internship.Services.TelemetryAPI.Models;

namespace TTMS.Internship.Services.TelemetryAPI.Services
{
    public class TelemetryService : ITelemetryService
    {
        private const string NoResultMessage = "Found no results for given device";

        private readonly ILogger<TelemetryService> logger;

        private readonly IClientRepository clientRepository;

        public TelemetryService(ILogger<TelemetryService> logger, IClientRepository clientRepository)
        {
            this.logger = logger;
            this.clientRepository = clientRepository;
        }

        public ResponseModel<TelemetryEntity> GetTelemetry(string deviceID, string startDate, string endDate)
        {
            try
            {
                this.logger.LogInformation("New request, Device: {0}, start date:{1}, end date: {2}", deviceID, startDate, endDate);

                var entities = this.clientRepository.QueryForTelemetries(deviceID, startDate, endDate);

                string message = NoResultMessage;

                this.logger.LogInformation("Found {0} matching objects in storage", entities.Count);

                var response = entities.Any() ? ResponseModel<TelemetryEntity>.CreateCorrectResponse(entities) : ResponseModel<TelemetryEntity>.CreateErrorResponse(message, this.logger, deviceID);

                return response;
            }
            catch (FormatException ex)
            {
                var response = ResponseModel<TelemetryEntity>.CreateErrorResponse(ex.Message, this.logger, deviceID);

                return response;
            }
            catch (InvalidOperationException ex)
            {
                var response = ResponseModel<TelemetryEntity>.CreateErrorResponse(ex.Message, this.logger, deviceID);

                return response;
            }
        }
    }
}
