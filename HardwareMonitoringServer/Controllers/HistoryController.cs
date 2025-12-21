using HardwareMonitoringServer.DbSetting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HardwareMonitoringServer.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? start, [FromQuery] DateTime? end)
        {
            var dateFrom = start ?? DateTime.UtcNow.AddDays(-1);
            var dateTo = end ?? DateTime.UtcNow;

            var data = await _context.Computers
                .AsNoTracking()
                .Include(c => c.Systems)
                    .ThenInclude(s => s.Sensors)
                .Where(c => c.Time >= dateFrom && c.Time <= dateTo)
                .OrderBy(c => c.Time)
                .ToListAsync();

            return Ok(data);
        }
    }
}
