using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolPortal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifiedAdminAnnouncementModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AdminAnnouncements",
                newName: "DatePosted");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEditted",
                table: "AdminAnnouncements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsEditted",
                table: "AdminAnnouncements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEditted",
                table: "AdminAnnouncements");

            migrationBuilder.DropColumn(
                name: "IsEditted",
                table: "AdminAnnouncements");

            migrationBuilder.RenameColumn(
                name: "DatePosted",
                table: "AdminAnnouncements",
                newName: "Date");
        }
    }
}
