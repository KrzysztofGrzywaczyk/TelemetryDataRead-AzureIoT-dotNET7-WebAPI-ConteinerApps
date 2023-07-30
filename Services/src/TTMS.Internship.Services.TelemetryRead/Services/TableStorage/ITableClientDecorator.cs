using Azure;
using Azure.Data.Tables;

namespace TTMS.Internship.Services.TelemetryRead.Services.TableStorage
{
    public interface ITableClientDecorator
    {
        Task<Response> AddEntityAsync(TableEntity entity);
    }
}
