using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class CreateRAMs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoryModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MemoryTypeId = table.Column<Guid>(nullable: false),
                    MemoryBytes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoryModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoryModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemoryModels_MemoryTypes_MemoryTypeId",
                        column: x => x.MemoryTypeId,
                        principalTable: "MemoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RAMModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RAMModels_MemoryModels_Id",
                        column: x => x.Id,
                        principalTable: "MemoryModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemRAMs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    RAMModelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRAMs", x => new { x.SystemId, x.RAMModelId });
                    table.ForeignKey(
                        name: "FK_SystemRAMs_RAMModels_RAMModelId",
                        column: x => x.RAMModelId,
                        principalTable: "RAMModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemRAMs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoryModels_MemoryTypeId",
                table: "MemoryModels",
                column: "MemoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRAMs_RAMModelId",
                table: "SystemRAMs",
                column: "RAMModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemRAMs");

            migrationBuilder.DropTable(
                name: "RAMModels");

            migrationBuilder.DropTable(
                name: "MemoryModels");

            migrationBuilder.DropTable(
                name: "MemoryTypes");
        }
    }
}
