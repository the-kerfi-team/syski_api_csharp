using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csharp.Data.Migrations
{
    public partial class AddTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TheUserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TokenType = table.Column<string>(nullable: true),
                    Issuer = table.Column<string>(nullable: true),
                    Audience = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    NotBefore = table.Column<DateTime>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    NextTokenId = table.Column<Guid>(nullable: true),
                    PreviousTokenId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheUserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheUserTokens_TheUserTokens_PreviousTokenId",
                        column: x => x.PreviousTokenId,
                        principalTable: "TheUserTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TheUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TheUserTokens_PreviousTokenId",
                table: "TheUserTokens",
                column: "PreviousTokenId",
                unique: true,
                filter: "[PreviousTokenId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TheUserTokens_UserId",
                table: "TheUserTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheUserTokens");
        }
    }
}
