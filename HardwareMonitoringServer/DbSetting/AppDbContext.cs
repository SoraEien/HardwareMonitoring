using HardwareMonitoringServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HardwareMonitoringServer.DbSetting;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ComputerModel> Computers { get; set; }
    public DbSet<SystemModel> Systems { get; set; }
    public DbSet<SensorModel> Sensors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SensorModel>()
            .Property(s => s.SensorTypeName)
            .HasConversion<string>();
    }
}