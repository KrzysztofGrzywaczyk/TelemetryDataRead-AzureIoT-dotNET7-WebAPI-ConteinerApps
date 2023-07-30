namespace Device.Modules.ReceiveAndSendModule.MessageOutput.MessageBodyBuilder
{
    public class MessageBodyBuilder
    {
        private MessageBody messageBody = new MessageBody();

        public MessageBody Build()
        {
            return this.messageBody;
        }

        public MessageBodyBuilder SetCo2Value(double co2Value)
        {
            this.messageBody.Co2 = co2Value;
            return this;
        }

        public MessageBodyBuilder SetDeviceId(string? deviceId)
        {
            this.messageBody.DeviceID = deviceId;
            return this;
        }

        public MessageBodyBuilder SetHumidity(double humidity)
        {
            this.messageBody.Humidity = humidity;
            return this;
        }

        public MessageBodyBuilder SetPressure(double pressure)
        {
            this.messageBody.Pressure = pressure;
            return this;
        }

        public MessageBodyBuilder SetTemperature(double temperature)
        {
            this.messageBody.Temperature = temperature;
            return this;
        }

        public MessageBodyBuilder SetTimeCreated(DateTime timeCreated)
        {
            this.messageBody.TimeCreated = timeCreated;
            return this;
        }
    }
}
