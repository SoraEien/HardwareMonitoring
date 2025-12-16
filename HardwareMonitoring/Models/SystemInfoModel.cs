using LibreHardwareMonitor.Hardware;

namespace HardwareMonitoring.Models
{
    public class SystemInfoModel
    {
        private string _name;
        private float? _value;
        private SensorType _type;

        public string Name { get => _name; set => _name = value; }
        public float? Value { get => _value; set => _value = value; }
        public SensorType SensorTypeName { get => _type; set => _type = value; }
    }
}
