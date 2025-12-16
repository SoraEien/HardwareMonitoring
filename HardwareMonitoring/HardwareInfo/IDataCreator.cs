using HardwareMonitoring.Models;

namespace HardwareMonitoring.HardwareInfo
{
    public interface IDataCreator
    {
        IEnumerable<SystemInfoModel> CreateEnumerable();
    }
}