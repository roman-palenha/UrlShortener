using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AboutMessages",
                columns: new[] { "Id", "Message" },
                values: new object[] { 1, "\r\nTo short url I used a Bijective Function f. \r\nIf you want more, visit <a>https://en.wikipedia.org/wiki/Bijection</a>" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { 1, "admin@gmail.com", "2af9b1ba42dc5eb01743e6b3759b6e4b", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AboutMessages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
