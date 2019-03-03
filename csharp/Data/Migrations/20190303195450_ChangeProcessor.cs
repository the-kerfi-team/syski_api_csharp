using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class ChangeProcessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GPUModels_ProcessorModels_Id",
                table: "GPUModels");

            migrationBuilder.DropColumn(
                name: "ClockSpeed",
                table: "ProcessorModels");

            migrationBuilder.DropColumn(
                name: "CoreCount",
                table: "ProcessorModels");

            migrationBuilder.DropColumn(
                name: "ThreadCount",
                table: "ProcessorModels");

            migrationBuilder.AddColumn<int>(
                name: "ClockSpeed",
                table: "SystemCPUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoreCount",
                table: "SystemCPUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThreadCount",
                table: "SystemCPUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "GPUModelId",
                table: "ProcessorModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorModels_GPUModelId",
                table: "ProcessorModels",
                column: "GPUModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessorModels_GPUModels_GPUModelId",
                table: "ProcessorModels",
                column: "GPUModelId",
                principalTable: "GPUModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessorModels_GPUModels_GPUModelId",
                table: "ProcessorModels");

            migrationBuilder.DropIndex(
                name: "IX_ProcessorModels_GPUModelId",
                table: "ProcessorModels");

            migrationBuilder.DropColumn(
                name: "ClockSpeed",
                table: "SystemCPUs");

            migrationBuilder.DropColumn(
                name: "CoreCount",
                table: "SystemCPUs");

            migrationBuilder.DropColumn(
                name: "ThreadCount",
                table: "SystemCPUs");

            migrationBuilder.DropColumn(
                name: "GPUModelId",
                table: "ProcessorModels");

            migrationBuilder.AddColumn<int>(
                name: "ClockSpeed",
                table: "ProcessorModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoreCount",
                table: "ProcessorModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThreadCount",
                table: "ProcessorModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GPUModels_ProcessorModels_Id",
                table: "GPUModels",
                column: "Id",
                principalTable: "ProcessorModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
