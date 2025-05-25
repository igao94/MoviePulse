using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCelebrityRoleToMovieRoleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelebrityRoles");

            migrationBuilder.CreateTable(
                name: "MovieRoles",
                columns: table => new
                {
                    CelebrityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRoles", x => new { x.MovieId, x.CelebrityId, x.RoleTypeId });
                    table.ForeignKey(
                        name: "FK_MovieRoles_Celebrities_CelebrityId",
                        column: x => x.CelebrityId,
                        principalTable: "Celebrities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovieRoles_CelebrityRoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "CelebrityRoleTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovieRoles_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieRoles_CelebrityId",
                table: "MovieRoles",
                column: "CelebrityId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRoles_RoleTypeId",
                table: "MovieRoles",
                column: "RoleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieRoles");

            migrationBuilder.CreateTable(
                name: "CelebrityRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CelebrityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrityRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CelebrityRoles_Celebrities_CelebrityId",
                        column: x => x.CelebrityId,
                        principalTable: "Celebrities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CelebrityRoles_CelebrityRoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "CelebrityRoleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelebrityRoles_CelebrityId",
                table: "CelebrityRoles",
                column: "CelebrityId");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrityRoles_RoleTypeId",
                table: "CelebrityRoles",
                column: "RoleTypeId");
        }
    }
}
