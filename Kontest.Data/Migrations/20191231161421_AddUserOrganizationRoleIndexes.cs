using Microsoft.EntityFrameworkCore.Migrations;

namespace Kontest.Data.Migrations
{
    public partial class AddUserOrganizationRoleIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserOrganizations_OrganizationId",
                table: "UserOrganizations");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizations_OrganizationId_UserId_OrgnizationUserRoleType",
                table: "UserOrganizations",
                columns: new[] { "OrganizationId", "UserId", "OrgnizationUserRoleType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserOrganizations_OrganizationId_UserId_OrgnizationUserRoleType",
                table: "UserOrganizations");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizations_OrganizationId",
                table: "UserOrganizations",
                column: "OrganizationId");
        }
    }
}
