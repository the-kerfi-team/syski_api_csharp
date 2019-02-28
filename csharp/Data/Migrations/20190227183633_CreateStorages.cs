using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class CreateStorages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageModels_MemoryModels_Id",
                        column: x => x.Id,
                        principalTable: "MemoryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemStorages",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    StorageModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStorages", x => new { x.SystemId, x.StorageModelId });
                    table.ForeignKey(
                        name: "FK_SystemStorages_StorageModels_StorageModelId",
                        column: x => x.StorageModelId,
                        principalTable: "StorageModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemStorages_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemStorages_StorageModelId",
                table: "SystemStorages",
                column: "StorageModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemStorages");

            migrationBuilder.DropTable(
                name: "StorageModels");
        }
    }
}
