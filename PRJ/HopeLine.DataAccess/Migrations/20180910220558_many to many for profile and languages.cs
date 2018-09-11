using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class manytomanyforprofileandlanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profile_UserAccount_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Profile_ProfileId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageName",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Languages",
                newName: "ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_ProfileId",
                table: "Languages",
                newName: "IX_Languages_ConversationId");

            migrationBuilder.AlterColumn<string>(
                name: "CountryOrigin",
                table: "Languages",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Languages",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProfileLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileLanguage", x => new { x.LanguageId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ProfileLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileLanguage_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLanguage_ProfileId",
                table: "ProfileLanguage",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profile_UserAccount_ProfileId",
                table: "AspNetUsers",
                column: "UserAccount_ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Conversations_ConversationId",
                table: "Languages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profile_UserAccount_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Conversations_ConversationId",
                table: "Languages");

            migrationBuilder.DropTable(
                name: "ProfileLanguage");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "Languages",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_ConversationId",
                table: "Languages",
                newName: "IX_Languages_ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "CountryOrigin",
                table: "Languages",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AddColumn<string>(
                name: "LanguageName",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profile_UserAccount_ProfileId",
                table: "AspNetUsers",
                column: "UserAccount_ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Profile_ProfileId",
                table: "Languages",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
