using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class UpdateProcessorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessorModels_Models_Id",
                table: "ProcessorModels");

            migrationBuilder.AddColumn<Guid>(
                name: "ModelId",
                table: "ProcessorModels",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorModels_ModelId",
                table: "ProcessorModels",
                column: "ModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessorModels_Models_ModelId",
                table: "ProcessorModels",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessorModels_Models_ModelId",
                table: "ProcessorModels");

            migrationBuilder.DropIndex(
                name: "IX_ProcessorModels_ModelId",
                table: "ProcessorModels");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "ProcessorModels");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessorModels_Models_Id",
                table: "ProcessorModels",
                column: "Id",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
