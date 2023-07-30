namespace Device.Modules.TemperatureSensorModule
{
    public interface ISensor
    {
        public MessageBody ReadDataFromSensor();
    }
}
