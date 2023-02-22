using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprout.Exam.DataAccess.Migrations
{
    public partial class RemoveDuplicatedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EmployeeTypes",
                newName: "EmployeeType");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameColumn(
                name: "Tin",
                table: "Employee",
                newName: "TIN");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "EmployeeType",
                newName: "EmployeeTypes");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameColumn(
                name: "TIN",
                table: "Employees",
                newName: "Tin");

        }
    }
}
