using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class CreateOSs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemModel_Models_Id",
                table: "SystemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemModel_SystemTypes_TypeId",
                table: "SystemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Systems_SystemModel_ModelId",
                table: "Systems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemModel",
                table: "SystemModel");

            migrationBuilder.RenameTable(
                name: "SystemModel",
                newName: "SystemModels");

            migrationBuilder.RenameIndex(
                name: "IX_SystemModel_TypeId",
                table: "SystemModels",
                newName: "IX_SystemModels_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemModels",
                table: "SystemModels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OperatingSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemOSs",
                columns: table => new
                {
                    SystemId = table.Column<Guid>(nullable: false),
                    OSId = table.Column<Guid>(nullable: false),
                    ArchitectureId = table.Column<Guid>(nullable: false),
                    version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOSs", x => new { x.SystemId, x.OSId });
                    table.ForeignKey(
                        name: "FK_SystemOSs_Architectures_ArchitectureId",
                        column: x => x.ArchitectureId,
                        principalTable: "Architectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemOSs_OperatingSystems_OSId",
                        column: x => x.OSId,
                        principalTable: "OperatingSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemOSs_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemOSs_ArchitectureId",
                table: "SystemOSs",
                column: "ArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemOSs_OSId",
                table: "SystemOSs",
                column: "OSId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemModels_Models_Id",
                table: "SystemModels",
                column: "Id",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemModels_SystemTypes_TypeId",
                table: "SystemModels",
                column: "TypeId",
                principalTable: "SystemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_SystemModels_ModelId",
                table: "Systems",
                column: "ModelId",
                principalTable: "SystemModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemModels_Models_Id",
                table: "SystemModels");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemModels_SystemTypes_TypeId",
                table: "SystemModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Systems_SystemModels_ModelId",
                table: "Systems");

            migrationBuilder.DropTable(
                name: "SystemOSs");

            migrationBuilder.DropTable(
                name: "OperatingSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemModels",
                table: "SystemModels");

            migrationBuilder.RenameTable(
                name: "SystemModels",
                newName: "SystemModel");

            migrationBuilder.RenameIndex(
                name: "IX_SystemModels_TypeId",
                table: "SystemModel",
                newName: "IX_SystemModel_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemModel",
                table: "SystemModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemModel_Models_Id",
                table: "SystemModel",
                column: "Id",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemModel_SystemTypes_TypeId",
                table: "SystemModel",
                column: "TypeId",
                principalTable: "SystemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_SystemModel_ModelId",
                table: "Systems",
                column: "ModelId",
                principalTable: "SystemModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
