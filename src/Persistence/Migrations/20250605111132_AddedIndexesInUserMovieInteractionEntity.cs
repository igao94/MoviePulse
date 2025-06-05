using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexesInUserMovieInteractionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserMovieInteractions_IsInWatchlist",
                table: "UserMovieInteractions",
                column: "IsInWatchlist");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieInteractions_IsWatched",
                table: "UserMovieInteractions",
                column: "IsWatched");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieInteractions_Rating",
                table: "UserMovieInteractions",
                column: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserMovieInteractions_IsInWatchlist",
                table: "UserMovieInteractions");

            migrationBuilder.DropIndex(
                name: "IX_UserMovieInteractions_IsWatched",
                table: "UserMovieInteractions");

            migrationBuilder.DropIndex(
                name: "IX_UserMovieInteractions_Rating",
                table: "UserMovieInteractions");
        }
    }
}
