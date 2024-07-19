using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inforce_Api.Migrations
{
    /// <inheritdoc />
    public partial class shortenUrlModelFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShortenUrls_Code",
                table: "ShortenUrls",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShortenUrls_Code",
                table: "ShortenUrls");
        }
    }
}
