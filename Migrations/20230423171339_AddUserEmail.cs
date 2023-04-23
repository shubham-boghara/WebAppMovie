using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppMovie.Migrations
{
    public partial class AddUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eamil",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Eamil",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
