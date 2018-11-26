using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class ConversationModelRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_MentorId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Conversations_ConversationId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_ConversationId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_MentorId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Conversations",
                newName: "MentorAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Conversations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PIN",
                table: "Conversations",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "MentorId",
                table: "Conversations",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MentorAccountId",
                table: "Conversations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_MentorAccountId",
                table: "Conversations",
                column: "MentorAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_MentorAccountId",
                table: "Conversations",
                column: "MentorAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_MentorAccountId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_MentorAccountId",
                table: "Conversations");

            migrationBuilder.RenameColumn(
                name: "MentorAccountId",
                table: "Conversations",
                newName: "UserName");

            migrationBuilder.AddColumn<int>(
                name: "ConversationId",
                table: "Languages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Conversations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PIN",
                table: "Conversations",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "MentorId",
                table: "Conversations",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Conversations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ConversationId",
                table: "Languages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_MentorId",
                table: "Conversations",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_MentorId",
                table: "Conversations",
                column: "MentorId",
                principalTable: "AspNetUsers",
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
    }
}
