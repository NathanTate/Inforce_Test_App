using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inforce_Api.Migrations
{
    /// <inheritdoc />
    public partial class shortenUrlModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortenUrls_AspNetUsers_ApplicationUserId",
                table: "ShortenUrls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShortenUrls");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "ShortenUrls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShortenUrls_AspNetUsers_ApplicationUserId",
                table: "ShortenUrls",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortenUrls_AspNetUsers_ApplicationUserId",
                table: "ShortenUrls");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "ShortenUrls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShortenUrls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShortenUrls_AspNetUsers_ApplicationUserId",
                table: "ShortenUrls",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
