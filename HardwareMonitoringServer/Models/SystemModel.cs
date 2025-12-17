namespace HardwareMonitoringServer.Models
{
    public class SystemModel : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ComputerModelId { get; set; }
        public List<SensorModel> Sensors { get; set; } = new();
    }
}
