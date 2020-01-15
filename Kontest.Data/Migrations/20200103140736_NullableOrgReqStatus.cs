using Microsoft.EntityFrameworkCore.Migrations;

namespace Kontest.Data.Migrations
{
    public partial class NullableOrgReqStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrgRequestStatus",
                table: "OrganizationRequests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrgRequestStatus",
                table: "OrganizationRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
