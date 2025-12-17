using HardwareMonitoringServer.Models;

public class ComputerModel : IEntity<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<SystemModel> Systems { get; set; } = new();
    public DateTime Time { get; set; }
}
