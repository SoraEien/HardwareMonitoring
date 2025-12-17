using HardwareMonitoringServer.Monitor;
using Microsoft.AspNetCore.Mvc;

namespace HardwareMonitoringServer.Controllers
{
    [ApiController]
    [Route("api/monitor")] // Задает базовый адрес: http://localhost:5000/api/monitor
    public class MonitorController : ControllerBase
    {
        private readonly DataMonitoring _dataMonitor;
        public MonitorController(DataMonitoring dataReceiver)
        {
            _dataMonitor = dataReceiver;
        }

        [HttpPost]
        public IActionResult ReceiveData([FromBody] ComputerModel data)
        {
            _dataMonitor.AddSys(data);

            return Ok(new { message = "Data received successfully" });
        }
    }
}
