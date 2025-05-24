using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CelebrityRoleEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelebrityRoles");
        }
    }
}
