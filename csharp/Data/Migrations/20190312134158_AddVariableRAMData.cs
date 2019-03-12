using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddVariableRAMData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemRAMData",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    Free = table.Column<int>(nullable: false),
                    CollectionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRAMData", x => new { x.SystemId, x.CollectionDateTime });
                    table.ForeignKey(
                        name: "FK_SystemRAMData_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemRAMData");
        }
    }
}
