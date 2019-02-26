using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddUserAndSystemRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserSystems",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SystemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSystems", x => new { x.UserId, x.SystemId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSystems_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSystems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSystems_SystemId",
                table: "ApplicationUserSystems",
                column: "SystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSystems");
        }
    }
}
