using HardwareMonitoringServer.DbSetting;
using HardwareMonitoringServer.Monitor;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=hardware_monitor.db"));

            builder.Services.AddSingleton<DataMonitoring>();

            builder.Services.AddControllers();

            builder.WebHost.UseUrls(baseUrl);

            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapControllers();

            var dataMonitoringService = app.Services.GetRequiredService<DataMonitoring>();

            TimerCallback timerDelegate = _ =>
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var monitor = scope.ServiceProvider.GetRequiredService<DataMonitoring>();

                    var averagedData = monitor.GetAveragedData();
                    if (averagedData.Any())
                    {
                        db.Computers.AddRange(averagedData);
                        db.SaveChanges();
                        monitor.ClearSys();
                        Console.WriteLine($"Saved {averagedData.Count} records to DB.");
                    }
                }
            };

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
