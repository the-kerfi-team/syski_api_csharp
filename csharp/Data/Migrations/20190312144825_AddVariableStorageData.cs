using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddVariableStorageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Load",
                table: "SystemNetworkData");

            migrationBuilder.CreateTable(
                name: "SystemStorageData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Time = table.Column<float>(nullable: false),
                    Transfers = table.Column<float>(nullable: false),
                    Reads = table.Column<float>(nullable: false),
                    Writes = table.Column<float>(nullable: false),
                    ByteReads = table.Column<float>(nullable: false),
                    ByteWrites = table.Column<float>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStorageData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemStorageData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemStorageData");

            migrationBuilder.AddColumn<double>(
                name: "Load",
                table: "SystemNetworkData",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
