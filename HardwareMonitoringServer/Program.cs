using HardwareMonitoringServer.Monitor;

namespace HardwareMonitoringServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<DataMonitoring>();

            builder.Services.AddControllers();

            builder.WebHost.UseUrls("http://localhost:5050");

            var app = builder.Build();

            app.MapControllers();

            Console.WriteLine("Server started with Controllers...");

            app.Run();
        }
    }
}
