using LibreHardwareMonitor.Hardware;

namespace HardwareMonitoringServer.Models
{
    public class SensorModel
    {
        public string Name { get; set; }
        public float? Value { get; set; }
        public SensorType SensorTypeName { get; set; }
    }
}
