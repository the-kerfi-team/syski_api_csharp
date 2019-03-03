using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class FixSystemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Systems_ModelId",
                table: "Systems");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ModelId",
                table: "Systems",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Systems_ModelId",
                table: "Systems");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ModelId",
                table: "Systems",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");
        }
    }
}
