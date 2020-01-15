using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kontest.Data.Migrations
{
    public partial class ChangeForeignKeyNameRequestingUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationRequests_ApplicationUsers_UserId",
                table: "OrganizationRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationRequests_UserId",
                table: "OrganizationRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrganizationRequests");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequests_RequestingUserId",
                table: "OrganizationRequests",
                column: "RequestingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationRequests_ApplicationUsers_RequestingUserId",
                table: "OrganizationRequests",
                column: "RequestingUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationRequests_ApplicationUsers_RequestingUserId",
                table: "OrganizationRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationRequests_RequestingUserId",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "OrganizationRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequests_UserId",
                table: "OrganizationRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationRequests_ApplicationUsers_UserId",
                table: "OrganizationRequests",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
