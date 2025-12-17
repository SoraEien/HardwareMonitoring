using HardwareMonitoringServer.Monitor;

namespace HardwareMonitoringServer
{
    class Program
    {
        private static Timer _clearTimer;

        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var clearInterval = long.Parse(configuration["TimerSettings:ClearInterval"]
                                           ?? throw new InvalidOperationException("ClearInterval not found."));
            var baseUrl = configuration["Base:Url"] ?? "http://localhost:5050";


            builder.Services.AddSingleton<DataMonitoring>();

            builder.Services.AddControllers();

            builder.WebHost.UseUrls(baseUrl);

            var app = builder.Build();

            app.MapControllers();

            var dataMonitoringService = app.Services.GetRequiredService<DataMonitoring>();

            TimerCallback timerDelegate = _ => dataMonitoringService.ClearSys();

            _clearTimer = new Timer(timerDelegate,
                                    null,
                                    0,
                                    clearInterval);

            Console.WriteLine($"Server started with Controllers...");
            Console.WriteLine($"Data clearing timer set to fire every {clearInterval} ms.");

            app.Run();
        }
    }
}
