using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolPortal.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class separatedFileUrlFromAdminAnnouncement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "AdminAnnouncements");

            migrationBuilder.CreateTable(
                name: "AdminAnnouncementFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminAnnouncementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAnnouncementFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminAnnouncementFiles_AdminAnnouncements_AdminAnnouncementId",
                        column: x => x.AdminAnnouncementId,
                        principalTable: "AdminAnnouncements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminAnnouncementFiles_AdminAnnouncementId",
                table: "AdminAnnouncementFiles",
                column: "AdminAnnouncementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAnnouncementFiles");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "AdminAnnouncements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
