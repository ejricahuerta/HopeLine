using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class newtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profile_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorSpecialization_AspNetUsers_MentorAccountId",
                table: "MentorSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorSpecialization_Specializations_SpecializationId",
                table: "MentorSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileLanguage_Languages_LanguageId",
                table: "ProfileLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileLanguage_Profile_ProfileId",
                table: "ProfileLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileLanguage",
                table: "ProfileLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profile",
                table: "Profile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MentorSpecialization",
                table: "MentorSpecialization");

            migrationBuilder.RenameTable(
                name: "ProfileLanguage",
                newName: "ProfileLanguages");

            migrationBuilder.RenameTable(
                name: "Profile",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "MentorSpecialization",
                newName: "MentorSpecializations");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileLanguage_LanguageId",
                table: "ProfileLanguages",
                newName: "IX_ProfileLanguages_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_MentorSpecialization_SpecializationId",
                table: "MentorSpecializations",
                newName: "IX_MentorSpecializations_SpecializationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileLanguages",
                table: "ProfileLanguages",
                columns: new[] { "ProfileId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MentorSpecializations",
                table: "MentorSpecializations",
                columns: new[] { "MentorAccountId", "SpecializationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profiles_ProfileId",
                table: "AspNetUsers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MentorSpecializations_AspNetUsers_MentorAccountId",
                table: "MentorSpecializations",
                column: "MentorAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MentorSpecializations_Specializations_SpecializationId",
                table: "MentorSpecializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileLanguages_Languages_LanguageId",
                table: "ProfileLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileLanguages_Profiles_ProfileId",
                table: "ProfileLanguages",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profiles_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorSpecializations_AspNetUsers_MentorAccountId",
                table: "MentorSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_MentorSpecializations_Specializations_SpecializationId",
                table: "MentorSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileLanguages_Languages_LanguageId",
                table: "ProfileLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileLanguages_Profiles_ProfileId",
                table: "ProfileLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileLanguages",
                table: "ProfileLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MentorSpecializations",
                table: "MentorSpecializations");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profile");

            migrationBuilder.RenameTable(
                name: "ProfileLanguages",
                newName: "ProfileLanguage");

            migrationBuilder.RenameTable(
                name: "MentorSpecializations",
                newName: "MentorSpecialization");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileLanguages_LanguageId",
                table: "ProfileLanguage",
                newName: "IX_ProfileLanguage_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_MentorSpecializations_SpecializationId",
                table: "MentorSpecialization",
                newName: "IX_MentorSpecialization_SpecializationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profile",
                table: "Profile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileLanguage",
                table: "ProfileLanguage",
                columns: new[] { "ProfileId", "LanguageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MentorSpecialization",
                table: "MentorSpecialization",
                columns: new[] { "MentorAccountId", "SpecializationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profile_ProfileId",
                table: "AspNetUsers",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MentorSpecialization_AspNetUsers_MentorAccountId",
                table: "MentorSpecialization",
                column: "MentorAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MentorSpecialization_Specializations_SpecializationId",
                table: "MentorSpecialization",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileLanguage_Languages_LanguageId",
                table: "ProfileLanguage",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileLanguage_Profile_ProfileId",
                table: "ProfileLanguage",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
