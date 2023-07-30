using UnitsNet;

namespace Device.Modules.Co2SensorModule
{
    public interface ISensor
    {
        public VolumeConcentration GetConcentration();
    }
}
