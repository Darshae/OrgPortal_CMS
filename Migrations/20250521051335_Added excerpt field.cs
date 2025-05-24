using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgPortal_CMS.Migrations
{
    /// <inheritdoc />
    public partial class Addedexcerptfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Excerpt",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excerpt",
                table: "Announcements");
        }
    }
}
