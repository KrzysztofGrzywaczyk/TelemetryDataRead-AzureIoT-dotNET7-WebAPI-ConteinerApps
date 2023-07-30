using Service.TelemetryRead.Entities;

namespace TTMS.Internship.Services.TelemetryRead.Extensions
{
    public static class MessageEntityExtension
    {
        public static string PartitionKeyGenerate(this MessageEntity message)
        {
            return $"{message.TimeCreated.ToUniversalTime():yyyy-MM-dd:HH}_{message.DeviceID}";
        }

        public static string RowKeyGenerate(this MessageEntity message)
        {
            return $"{DateTime.MaxValue.Ticks - message.TimeCreated.Ticks:d19}_{Guid.NewGuid():n}";
        }
    }
}
