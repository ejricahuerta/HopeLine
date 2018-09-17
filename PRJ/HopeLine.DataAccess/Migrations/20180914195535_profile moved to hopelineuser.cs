using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class profilemovedtohopelineuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profile_UserAccount_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserAccount_ProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAccount_ProfileId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccount_ProfileId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserAccount_ProfileId",
                table: "AspNetUsers",
                column: "UserAccount_ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profile_UserAccount_ProfileId",
                table: "AspNetUsers",
                column: "UserAccount_ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
