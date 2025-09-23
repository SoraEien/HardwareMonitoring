using HardwareMonitoringClient.HardwareInfo;
using HardwareTemperature.HardwareInfo;
using Microsoft.Extensions.Configuration;

namespace HardwareMonitoringClient
{
    class Program
    {
        private static Timer timer;
        private static HardwareDisplay temperatureDisplay;

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            long interval = long.Parse(configuration["TimerSettings:Interval"]);

            HardwareManager hardwareManager = new HardwareManager();
            temperatureDisplay = new HardwareDisplay(hardwareManager);

            // Инициализация таймера
            timer = new Timer(TimerCallback, null, 0, interval);

            // Ждем нажатия Enter для выхода
            Console.ReadLine();

            // Остановка таймера и освобождение ресурсов
            StopTimer();
            hardwareManager.Close();
        }

        private static void TimerCallback(object state)
        {
            // Вызываем метод для отображения значений датчиков
            temperatureDisplay.DisplaySensorsValues();
        }

        private static void StopTimer()
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite);
            timer?.Dispose();
        }
        //static void Main(string[] args)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    long interval = long.Parse(configuration["TimerSettings:Interval"]);

        //    HardwareManager hardwareManager = new HardwareManager();
        //    HardwareDisplay temperatureDisplay = new HardwareDisplay(hardwareManager);

        //    Timer timer = new Timer();
        //    timer.Enabled = true;
        //    timer.Interval = interval;
        //    timer.Elapsed += (sender, e) => temperatureDisplay.DisplaySensorsValues();

        //    // Ждем нажатия Enter для выхода
        //    Console.ReadLine();
        //    hardwareManager.Close();
        //}
    }
}
