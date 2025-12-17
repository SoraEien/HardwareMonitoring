using HardwareMonitoringServer.Models;

namespace HardwareMonitoringServer.Monitor
{
    public class DataMonitoring
    {
        private List<ComputerModel> _systems = new List<ComputerModel>();

        public void AddSys(ComputerModel model) => _systems.Add(model);
        public void AddSys(IEnumerable<ComputerModel> model) => _systems.AddRange(model);
        public void ClearSys() => _systems.Clear();

        public IEnumerable<ComputerModel> GetSysData() => _systems;
        public List<ComputerModel> GetAveragedData() => _systems.GroupBy(c => c.Name)
                        .Select(compGroup => new ComputerModel
                        {
                            Name = compGroup.Key,
                            Time = DateTime.UtcNow,
                            Systems = compGroup
                                .SelectMany(c => c.Systems)
                                .GroupBy(s => s.Name)
                                .Select(sysGroup => new SystemModel
                                {
                                    Name = sysGroup.Key,
                                    Sensors = sysGroup
                                        .SelectMany(s => s.Sensors)
                                        .GroupBy(sn => new { sn.Name, sn.SensorTypeName })
                                        .Select(sensorGroup => new SensorModel
                                        {
                                            Name = sensorGroup.Key.Name,
                                            SensorTypeName = sensorGroup.Key.SensorTypeName,
                                            Value = sensorGroup.Average(v => v.Value)
                                        }).ToList()
                                }).ToList()
                        }).ToList();
    }
}
