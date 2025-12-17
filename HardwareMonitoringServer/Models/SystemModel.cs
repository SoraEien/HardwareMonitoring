namespace HardwareMonitoringServer.Models
{
    public class SystemModel
    {
        public string Name { get; set; }
        public List<SensorModel> Sensors { get; set; }
        public DateTime DateTime { get; set; }
    }
}
