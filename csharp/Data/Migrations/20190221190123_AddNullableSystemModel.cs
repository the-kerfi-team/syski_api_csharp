using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddNullableSystemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ModelId",
                table: "Systems",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ModelId",
                table: "Systems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
