using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "UserInfo",
                newName: "Gender");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserInfo");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "UserInfo",
                newName: "gender");
        }
    }
}
