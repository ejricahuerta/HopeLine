using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class Addratingtocoversation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Conversations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Conversations");
        }
    }
}
