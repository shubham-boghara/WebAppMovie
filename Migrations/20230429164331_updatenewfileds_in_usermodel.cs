using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppMovie.Migrations
{
    public partial class updatenewfileds_in_usermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailVerified",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpireOn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExpireOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Otp",
                table: "Users");
        }
    }
}
