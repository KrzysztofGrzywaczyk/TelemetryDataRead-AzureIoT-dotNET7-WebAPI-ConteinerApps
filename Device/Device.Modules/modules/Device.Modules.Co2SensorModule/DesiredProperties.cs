namespace Device.Modules.Co2SensorModule
{
    public class DesiredProperties
    {
        private readonly bool sendData = true;

        private readonly int sendInterval = 15000;

        public bool SendData
        {
            get { return this.sendData; }
        }

        public int SendInterval
        {
            get { return this.sendInterval; }
        }
    }
}