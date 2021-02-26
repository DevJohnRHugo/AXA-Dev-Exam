using Microsoft.EntityFrameworkCore.Migrations;

namespace Pointwest.Test.Migrations
{
    public partial class AddIdAndChangeMobileTypeToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Applicants");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Applicants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PositionApplied",
                table: "Applicants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<double>(
                name: "Mobile",
                table: "Applicants",
                type: "float",
                maxLength: 15,
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Applicants");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Applicants",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PositionApplied",
                table: "Applicants",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
