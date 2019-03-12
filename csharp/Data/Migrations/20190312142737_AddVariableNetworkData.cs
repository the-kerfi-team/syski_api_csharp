using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddVariableNetworkData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemNetworkData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Load = table.Column<double>(nullable: false),
                    Bandwidth = table.Column<float>(nullable: false),
                    Bytes = table.Column<float>(nullable: false),
                    Packets = table.Column<float>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNetworkData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemNetworkData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemNetworkData");
        }
    }
}
