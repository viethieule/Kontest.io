using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kontest.Data.Migrations
{
    public partial class AddOtacToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationRequests_Organizations_OrganizationId",
                table: "OrganizationRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationRequests_OrganizationId",
                table: "OrganizationRequests");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "OrganizationRequests");

            migrationBuilder.AddColumn<string>(
                name: "OTAC",
                table: "ApplicationUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTACExpires",
                table: "ApplicationUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequests_CreatedOrganizationId",
                table: "OrganizationRequests",
                column: "CreatedOrganizationId",
                unique: true,
                filter: "[CreatedOrganizationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationRequests_Organizations_CreatedOrganizationId",
                table: "OrganizationRequests",
                column: "CreatedOrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationRequests_Organizations_CreatedOrganizationId",
                table: "OrganizationRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationRequests_CreatedOrganizationId",
                table: "OrganizationRequests");

            migrationBuilder.DropColumn(
                name: "OTAC",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "OTACExpires",
                table: "ApplicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "OrganizationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRequests_OrganizationId",
                table: "OrganizationRequests",
                column: "OrganizationId",
                unique: true,
                filter: "[OrganizationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationRequests_Organizations_OrganizationId",
                table: "OrganizationRequests",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
