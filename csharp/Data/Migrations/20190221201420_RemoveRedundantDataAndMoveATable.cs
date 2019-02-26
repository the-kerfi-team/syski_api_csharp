using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class RemoveRedundantDataAndMoveATable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemModelTypes_SystemModels_SystemModelId",
                table: "SystemModelTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Systems_SystemModels_ModelId",
                table: "Systems");

            migrationBuilder.DropTable(
                name: "SystemModels");

            migrationBuilder.DropIndex(
                name: "IX_Systems_ModelId",
                table: "Systems");

            migrationBuilder.RenameColumn(
                name: "SystemModelId",
                table: "SystemModelTypes",
                newName: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ModelId",
                table: "Systems",
                column: "ModelId",
                unique: true,
                filter: "[ModelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemModelTypes_Systems_SystemId",
                table: "SystemModelTypes",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_Models_ModelId",
                table: "Systems",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemModelTypes_Systems_SystemId",
                table: "SystemModelTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Systems_Models_ModelId",
                table: "Systems");

            migrationBuilder.DropIndex(
                name: "IX_Systems_ModelId",
                table: "Systems");

            migrationBuilder.RenameColumn(
                name: "SystemId",
                table: "SystemModelTypes",
                newName: "SystemModelId");

            migrationBuilder.CreateTable(
                name: "SystemModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ModelId",
                table: "Systems",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemModelTypes_SystemModels_SystemModelId",
                table: "SystemModelTypes",
                column: "SystemModelId",
                principalTable: "SystemModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_SystemModels_ModelId",
                table: "Systems",
                column: "ModelId",
                principalTable: "SystemModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
