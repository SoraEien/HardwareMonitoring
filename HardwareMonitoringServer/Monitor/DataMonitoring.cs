using HardwareMonitoringServer.Models;

namespace HardwareMonitoringServer.Monitor
{
    public class DataMonitoring
    {
        private List<SystemModel> _systems = new List<SystemModel>();

        public void AddSys(SystemModel model) => _systems.Add(model);
        public void AddSys(IEnumerable<SystemModel> model) => _systems.AddRange(model);
        public void ClearSys() => _systems.Clear();

        public IEnumerable<SystemModel> GetSysData() => _systems;
    }
}
