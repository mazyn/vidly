using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class PopulateMovieGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Comedy" });

            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Action" });

            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Family" });

            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Romance" });

            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Drama" });

            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Horror" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE \"MovieGenre\" CASCADE");
        }
    }
}
