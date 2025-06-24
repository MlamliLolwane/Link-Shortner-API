using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShortnerAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReferrerFromLinksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalUrl",
                table: "Clicks");

            migrationBuilder.DropColumn(
                name: "ShortenedUrl",
                table: "Clicks");

            migrationBuilder.RenameColumn(
                name: "Referrer",
                table: "Links",
                newName: "ShortenedUrl");

            migrationBuilder.AddColumn<string>(
                name: "OriginalUrl",
                table: "Links",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalUrl",
                table: "Links");

            migrationBuilder.RenameColumn(
                name: "ShortenedUrl",
                table: "Links",
                newName: "Referrer");

            migrationBuilder.AddColumn<string>(
                name: "OriginalUrl",
                table: "Clicks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShortenedUrl",
                table: "Clicks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
