using HardwareMonitoring.HardwareInfo;
using HardwareMonitoring.HardwareInfo.Classes;
using Microsoft.Extensions.Configuration;

namespace HardwareMonitoringClient
{
    class Program
    {
        private static long _interval;
        private static bool _sendToServer;
        private static Timer _timer;
        private static IHardwareDisplay _temperatureDisplay;

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _interval = long.Parse(configuration["TimerSettings:Interval"]);
            _sendToServer = bool.Parse(configuration["Preset:SendToServer"]);

            IHardwareManager hardwareManager;
            if (_sendToServer)
                hardwareManager = new DataCreator();
            else
                hardwareManager = new HardwareManager();
            _temperatureDisplay = new HardwareDisplay(hardwareManager);

            // Инициализация таймера
            _timer = new Timer(TimerCallback, null, 0, _interval);

            // Ждем нажатия Enter для выхода
            Console.ReadLine();

            // Остановка таймера и освобождение ресурсов
            StopTimer();
            hardwareManager.Close();
        }

        private static void TimerCallback(object state)
        {
            // Вызываем метод для отображения значений датчиков
            _temperatureDisplay.DisplaySensorsValues();
            if (_sendToServer)
                _temperatureDisplay.SentToServer();
        }

        private static void StopTimer()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            _timer?.Dispose();
        }
    }
}
