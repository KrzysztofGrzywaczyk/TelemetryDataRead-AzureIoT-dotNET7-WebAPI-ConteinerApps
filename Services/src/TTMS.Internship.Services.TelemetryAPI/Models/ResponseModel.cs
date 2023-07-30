namespace TTMS.Internship.Services.TelemetryAPI.Models
{
    public class ResponseModel<T>
    {
        public bool HasError { get; set; } = false;

        public string? ErrorMessage { get; set; }

        public List<T>? Content { get; set; }

        public static ResponseModel<TelemetryEntity> CreateCorrectResponse(List<TelemetryEntity> entities)
        {
            var response = new ResponseModel<TelemetryEntity>()
            {
                HasError = false,
                Content = entities,
            };

            return response;
        }

        public static ResponseModel<TelemetryEntity> CreateErrorResponse(string message, ILogger logger, string deviceID)
        {
            var response = new ResponseModel<TelemetryEntity>()
            {
                HasError = true,
                ErrorMessage = message,
            };

            logger.LogError("Nieprawidłowe żądanie, Urządzenie: {0}", deviceID);
            logger.LogError("[BŁĄD] {0}", message);
            return response;
        }
    }
}
