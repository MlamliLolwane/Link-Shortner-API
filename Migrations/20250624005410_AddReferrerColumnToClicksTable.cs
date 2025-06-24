using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShortnerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReferrerColumnToClicksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Referrer",
                table: "Clicks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Referrer",
                table: "Clicks");
        }
    }
}
