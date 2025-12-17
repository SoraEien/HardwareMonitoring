using HardwareMonitoring.Models;
using HardwareMonitoring.Services;

namespace HardwareMonitoring.HardwareInfo.Classes
{
    public class HardwareDisplay : IHardwareDisplay
    {
        private readonly DataSender _dataSender;
        private readonly IHardwareManager _hardwareManager;

        public HardwareDisplay(IHardwareManager manager, string serverUrl)
        {
            _hardwareManager = manager;
            _dataSender = new DataSender(serverUrl);
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
            if (_hardwareManager is IDataCreator creator)
            {
                creator.Update();
                var dataEnumerable = creator.CreateEnumerable();
                var dataList = dataEnumerable.ToList();

                if (dataList.Any())
                {
                    await _dataSender.SendDataAsync(dataList);
                }
            }
        }
    }
}
