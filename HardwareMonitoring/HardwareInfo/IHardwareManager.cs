
namespace HardwareMonitoring.HardwareInfo
{
    public interface IHardwareManager
    {
        void Close();
        void Update();
        Task Monitor();
    }
}