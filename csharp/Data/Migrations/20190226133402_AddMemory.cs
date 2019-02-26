using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddMemory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_MemoryModels_Models_Id",
                table: "MemoryModels",
                column: "Id",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemoryModels_MemoryTypes_MemoryTypeId",
                table: "MemoryModels",
                column: "MemoryTypeId",
                principalTable: "MemoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemoryModels_Models_Id",
                table: "MemoryModels");

            migrationBuilder.DropForeignKey(
                name: "FK_MemoryModels_MemoryTypes_MemoryTypeId",
                table: "MemoryModels");
        }
    }
}
