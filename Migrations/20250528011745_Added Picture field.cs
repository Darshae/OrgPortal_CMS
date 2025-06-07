using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgPortal_CMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedPicturefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Announcements",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Announcements");
        }
    }
}
