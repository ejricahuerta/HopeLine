using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HopeLine.DataAccess.Migrations
{
    public partial class scheduleincludedcolumnsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndPeriod",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LogoutTime",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MentorAccountId",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StarTime",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartPeriod",
                table: "Schedules",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "TotalHours",
                table: "Schedules",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "HopeLineUserId",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_MentorAccountId",
                table: "Schedules",
                column: "MentorAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_HopeLineUserId",
                table: "Activities",
                column: "HopeLineUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_HopeLineUserId",
                table: "Activities",
                column: "HopeLineUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_MentorAccountId",
                table: "Schedules",
                column: "MentorAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_HopeLineUserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_MentorAccountId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_MentorAccountId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Activities_HopeLineUserId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EndPeriod",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "LogoutTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MentorAccountId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StarTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StartPeriod",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "HopeLineUserId",
                table: "Activities");
        }
    }
}
