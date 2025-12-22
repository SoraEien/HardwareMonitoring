using HardwareMonitoring.HardwareInfo;
using HardwareMonitoring.HardwareInfo.Classes;
using Microsoft.Extensions.Configuration;

namespace HardwareMonitoring
{
    class Program
    {
        private static AppSettings _settings;
        private static Timer _timer;
        private static Timer _sendTimer;
        private static IHardwareDisplay _temperatureDisplay;

        static void Main(string[] args)
        {
            string name;
            CreateSettinfs();

            if (_settings.UsePcName)
            {
                Console.WriteLine("Input you name: ");
                name = Console.ReadLine();
            }
            else name = _settings.PcName;


            IHardwareManager hardwareManager;
            if (_settings.SendToServer)
                hardwareManager = new DataCreator();
            else
                hardwareManager = new HardwareManager();
            _temperatureDisplay = new HardwareDisplay(hardwareManager, _settings.ServerUrl, name);

            // Инициализация таймера
            _timer = new Timer(TimerCallback, null, 0, _settings.Interval);
            if (_settings.SendToServer)
                _sendTimer = new Timer(SendTimerCallback, null, 0, _settings.SendTnterval);

            // Ждем нажатия Enter для выхода
            Console.ReadLine();

            // Остановка таймера и освобождение ресурсов
            StopTimer();
            hardwareManager.Close();
        }

        private static void CreateSettinfs()
        {
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            _settings = new AppSettings(long.Parse(configuration["TimerSettings:Interval"]),
                                        long.Parse(configuration["TimerSettings:SendIntervar"]),
                                        bool.Parse(configuration["Preset:SendToServer"]),
                                        configuration["ServerSettings:Url"] ?? "http://localhost:5000",
                                        configuration["Preset:DefName"] ?? "notSet",
                                        bool.Parse(configuration["Preset:UseDefName"]));
        }

        private static void TimerCallback(object state)
        {
            Task.Run(async () =>
            {
                await _temperatureDisplay.DisplaySensorsValues();
            });
        }

        private static void SendTimerCallback(object state)
        {
            Task.Run(async () =>
            {
                await _temperatureDisplay.SentToServer();
            });
        }

        private static void StopTimer()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _sendTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
            _sendTimer?.Dispose();
        }
    }
}
