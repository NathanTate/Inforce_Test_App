using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inforce_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedFkUserIdProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShortenUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShortenUrls");
        }
    }
}
