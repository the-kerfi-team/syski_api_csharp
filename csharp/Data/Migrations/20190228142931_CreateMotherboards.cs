using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class CreateMotherboards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MotherboardId",
                table: "Systems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MotherboardModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherboardModels_Models_Id",
                        column: x => x.Id,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Systems_MotherboardId",
                table: "Systems",
                column: "MotherboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_MotherboardModels_MotherboardId",
                table: "Systems",
                column: "MotherboardId",
                principalTable: "MotherboardModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Systems_MotherboardModels_MotherboardId",
                table: "Systems");

            migrationBuilder.DropTable(
                name: "MotherboardModels");

            migrationBuilder.DropIndex(
                name: "IX_Systems_MotherboardId",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "MotherboardId",
                table: "Systems");
        }
    }
}
