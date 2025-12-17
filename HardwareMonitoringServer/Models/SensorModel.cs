using LibreHardwareMonitor.Hardware;

namespace HardwareMonitoringServer.Models
{
    public class SensorModel : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float? Value { get; set; }
        public SensorType SensorTypeName { get; set; }
        public int SystemModelId { get; set; }
    }
}
