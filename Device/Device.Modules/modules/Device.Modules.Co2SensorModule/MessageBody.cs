using UnitsNet;

namespace Device.Modules.Co2SensorModule
{
    internal class MessageBody
    {
        public MessageBody(VolumeConcentration co2)
        {
            this.Co2Value = co2.Value;
        }

        public double Co2Value { get; set; }
    }
}
