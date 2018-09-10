using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class removedidforprofileandlanguagemapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileLanguage",
                table: "ProfileLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ProfileLanguage_ProfileId",
                table: "ProfileLanguage");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "ProfileLanguage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProfileLanguage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileLanguage",
                table: "ProfileLanguage",
                columns: new[] { "ProfileId", "LanguageId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLanguage_LanguageId",
                table: "ProfileLanguage",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileLanguage",
                table: "ProfileLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ProfileLanguage_LanguageId",
                table: "ProfileLanguage");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "ProfileLanguage",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProfileLanguage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileLanguage",
                table: "ProfileLanguage",
                columns: new[] { "LanguageId", "ProfileId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLanguage_ProfileId",
                table: "ProfileLanguage",
                column: "ProfileId");
        }
    }
}
