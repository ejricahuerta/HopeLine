using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations.ResourcesDb
{
    public partial class addpropertiesforclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Resources",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Resources",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Resources",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Maps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Communities",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Communities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Communities",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Communities",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Communities");
        }
    }
}
