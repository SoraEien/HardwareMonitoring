using HardwareMonitoring.Models;

namespace HardwareMonitoring.HardwareInfo
{
    public interface IDataCreator
    {
        IEnumerable<SystemModel> CreateEnumerable();
    }
}