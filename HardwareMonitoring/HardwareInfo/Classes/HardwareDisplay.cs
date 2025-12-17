using HardwareMonitoring.Models;
using HardwareMonitoring.Services;

namespace HardwareMonitoring.HardwareInfo.Classes
{
    public class HardwareDisplay : IHardwareDisplay
    {
        private readonly string _name;
        private readonly DataSender _dataSender;
        private readonly IHardwareManager _hardwareManager;

        public HardwareDisplay(IHardwareManager manager, string serverUrl, string name)
        {
            _hardwareManager = manager;
            _dataSender = new DataSender(serverUrl);
            _name = name;
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
            if (_hardwareManager is IDataCreator creator)
            {
                creator.Update();
                var dataEnumerable = creator.CreateEnumerable();
                var dataList = dataEnumerable.ToList();

                var data = new ComputerModel(_name);

                data.Systems = dataList;

                if (dataList.Count != 0)
                {
                    await _dataSender.SendDataAsync(data);
                }
            }
        }
    }
}
