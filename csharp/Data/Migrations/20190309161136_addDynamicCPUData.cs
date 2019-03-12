using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class addDynamicCPUData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemCPUsData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Load = table.Column<double>(nullable: false),
                    Processes = table.Column<int>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCPUsData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemCPUsData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemCPUsData");
        }
    }
}
