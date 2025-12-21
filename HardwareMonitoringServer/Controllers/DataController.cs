using HardwareMonitoringServer.Monitor;
using Microsoft.AspNetCore.Mvc;

namespace HardwareMonitoringServer.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly DataMonitoring _monitoring;
        public DataController(DataMonitoring monitoring) => _monitoring = monitoring;

        [HttpGet]
        public IActionResult Get() => Ok(_monitoring.GetSysData());
    }
}