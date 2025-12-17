using HardwareMonitoring.Models;

namespace HardwareMonitoring.HardwareInfo
{
    public interface IDataCreator : IHardwareManager
    {
        IEnumerable<SystemModel> CreateEnumerable();
    }
}