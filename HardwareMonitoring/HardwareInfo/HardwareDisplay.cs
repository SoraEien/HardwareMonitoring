using HardwareMonitoringClient.HardwareInfo;
using System;
using System.Threading.Tasks;

namespace HardwareTemperature.HardwareInfo
{
    public class HardwareDisplay
    {
        private HardwareManager _hardwareManager;

        public HardwareDisplay(HardwareManager manager)
        {
            _hardwareManager = manager;
        }

        public async Task DisplaySensorsValues()
        {
            Console.Clear();
            Console.WriteLine("{0}\n", DateTime.Now);

            await _hardwareManager.Monitor();

            Console.WriteLine("Press Enter to exit");
        }
    }
}
