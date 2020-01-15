using Microsoft.EntityFrameworkCore.Migrations;

namespace Kontest.Data.Migrations
{
    public partial class AddOrgReqEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionTypeCode",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<int>(
                name: "OrgRequestStatus",
                table: "OrganizationRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrgRequestStatus",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<int>(
                name: "ActionTypeCode",
                table: "OrganizationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
