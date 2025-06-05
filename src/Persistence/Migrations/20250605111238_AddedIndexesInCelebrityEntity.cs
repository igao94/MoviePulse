using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexesInCelebrityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Celebrities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Celebrities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Celebrities_CreatedAt",
                table: "Celebrities",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Celebrities_FirstName",
                table: "Celebrities",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Celebrities_LastName",
                table: "Celebrities",
                column: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Celebrities_CreatedAt",
                table: "Celebrities");

            migrationBuilder.DropIndex(
                name: "IX_Celebrities_FirstName",
                table: "Celebrities");

            migrationBuilder.DropIndex(
                name: "IX_Celebrities_LastName",
                table: "Celebrities");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Celebrities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Celebrities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
