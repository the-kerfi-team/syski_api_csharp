using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class CreateGPUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPUModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUModels_ProcessorModels_Id",
                        column: x => x.Id,
                        principalTable: "ProcessorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GPUModels_RAMModels_Id",
                        column: x => x.Id,
                        principalTable: "RAMModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemGPUs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    GPUModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGPUs", x => new { x.SystemId, x.GPUModelId });
                    table.ForeignKey(
                        name: "FK_SystemGPUs_GPUModels_GPUModelId",
                        column: x => x.GPUModelId,
                        principalTable: "GPUModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemGPUs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemGPUs_GPUModelId",
                table: "SystemGPUs",
                column: "GPUModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemGPUs");

            migrationBuilder.DropTable(
                name: "GPUModels");
        }
    }
}
