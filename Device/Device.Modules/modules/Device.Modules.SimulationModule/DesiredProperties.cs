namespace Device.Modules.SimulationModule
{
    public class DesiredProperties
    {
        private bool sendData = true;

        private int sendInterval = 60000;

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