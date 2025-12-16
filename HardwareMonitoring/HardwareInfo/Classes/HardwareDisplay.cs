using HardwareMonitoring.Models;

namespace HardwareMonitoring.HardwareInfo.Classes
{
    public class HardwareDisplay : IHardwareDisplay
    {
        private readonly IHardwareManager _hardwareManager;

        public HardwareDisplay(IHardwareManager manager)
        {
            _hardwareManager = manager;
        }

        public async Task DisplaySensorsValues()
        {
            Console.Clear();
            Console.WriteLine("{0}\n", DateTime.Now);

            _hardwareManager.Update();
            await _hardwareManager.Monitor();

            Console.WriteLine("Press Enter to exit");
        }

        public async Task SentToServer()
        {
            var data = new List<SystemModel>();
            if (_hardwareManager is DataCreator creator)
            {
                creator.Update();
                data.AddRange(creator.CreateEnumerable());
            }
            Console.WriteLine("Sent to server");
        }
    }
}
