using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardwareMonitoringServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ComputerModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerModelId1 = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Systems_Computers_ComputerModelId1",
                        column: x => x.ComputerModelId1,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<float>(type: "REAL", nullable: true),
                    SensorTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    SystemModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    SystemModelId1 = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Systems_SystemModelId1",
                        column: x => x.SystemModelId1,
                        principalTable: "Systems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SystemModelId1",
                table: "Sensors",
                column: "SystemModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ComputerModelId1",
                table: "Systems",
                column: "ComputerModelId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "Computers");
        }
    }
}
