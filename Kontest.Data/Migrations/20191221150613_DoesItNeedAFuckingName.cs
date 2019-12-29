using Microsoft.EntityFrameworkCore.Migrations;

namespace Kontest.Data.Migrations
{
    public partial class DoesItNeedAFuckingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleTypeCode",
                table: "UserOrganizations");

            migrationBuilder.AddColumn<int>(
                name: "OrgnizationUserRoleType",
                table: "UserOrganizations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Organizations",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgnizationUserRoleType",
                table: "UserOrganizations");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Organizations");

            migrationBuilder.AddColumn<int>(
                name: "RoleTypeCode",
                table: "UserOrganizations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
