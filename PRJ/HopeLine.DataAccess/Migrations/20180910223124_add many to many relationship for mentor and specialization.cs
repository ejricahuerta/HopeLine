using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class addmanytomanyrelationshipformentorandspecialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specializations_SpecializationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SpecializationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Specializations",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Specializations",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MentorSpecialization",
                columns: table => new
                {
                    MentorAccountId = table.Column<string>(nullable: false),
                    SpecializationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorSpecialization", x => new { x.MentorAccountId, x.SpecializationId });
                    table.ForeignKey(
                        name: "FK_MentorSpecialization_AspNetUsers_MentorAccountId",
                        column: x => x.MentorAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MentorSpecialization_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentorSpecialization_SpecializationId",
                table: "MentorSpecialization",
                column: "SpecializationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MentorSpecialization");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Specializations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Specializations",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SpecializationId",
                table: "AspNetUsers",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specializations_SpecializationId",
                table: "AspNetUsers",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
