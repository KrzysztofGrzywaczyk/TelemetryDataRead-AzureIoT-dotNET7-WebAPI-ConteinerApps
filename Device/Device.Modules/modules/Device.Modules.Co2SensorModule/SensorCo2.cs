using Iot.Device.Mhz19b;
using UnitsNet;

namespace Device.Modules.Co2SensorModule
{
    internal class SensorCo2 : ISensor
    {
        private readonly string devPath = "/dev/ttyS0";
        private readonly Mhz19b sensor;
        private readonly AbmState state = AbmState.On;
        private readonly DetectionRange range = DetectionRange.Range5000;

        public SensorCo2()
        {
            this.sensor = new Mhz19b(this.devPath);
            this.sensor.SetSensorDetectionRange(this.range);
            this.sensor.SetAutomaticBaselineCorrection(this.state);
            Console.WriteLine($"Sensor: {this.sensor.GetType()} #{this.sensor.GetHashCode()} initialized.");
        }

        public VolumeConcentration GetConcentration()
        {
            VolumeConcentration co2Concentration = this.sensor.GetCo2Reading();
            Console.WriteLine($"{co2Concentration.Value} {co2Concentration.Unit}");
            return co2Concentration;
        }
    }
}
