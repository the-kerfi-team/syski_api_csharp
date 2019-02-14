using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class CreateCPUs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Architectures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Architectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessorModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArchitectureId = table.Column<Guid>(nullable: false),
                    ClockSpeed = table.Column<double>(nullable: false),
                    CoreCount = table.Column<int>(nullable: false),
                    ThreadCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessorModels_Architectures_ArchitectureId",
                        column: x => x.ArchitectureId,
                        principalTable: "Architectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessorModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemCPUs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    CPUModelID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCPUs", x => new { x.CPUModelID, x.SystemId });
                    table.ForeignKey(
                        name: "FK_SystemCPUs_ProcessorModels_CPUModelID",
                        column: x => x.CPUModelID,
                        principalTable: "ProcessorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemCPUs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorModels_ArchitectureId",
                table: "ProcessorModels",
                column: "ArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCPUs_SystemId",
                table: "SystemCPUs",
                column: "SystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemCPUs");

            migrationBuilder.DropTable(
                name: "ProcessorModels");

            migrationBuilder.DropTable(
                name: "Architectures");
        }
    }
}
