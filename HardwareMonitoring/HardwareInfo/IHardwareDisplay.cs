
namespace HardwareMonitoring.HardwareInfo
{
    public interface IHardwareDisplay
    {
        Task DisplaySensorsValues();
        Task SentToServer();
    }
}