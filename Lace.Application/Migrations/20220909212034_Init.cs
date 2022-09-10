using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lace.Application.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lace_schema");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "lace_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "lace_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryElements",
                schema: "lace_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryElements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "lace_schema",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "lace_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lace_schema",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileAttributes",
                schema: "lace_schema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    DictionaryElementId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileAttributes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "lace_schema",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileAttributes_DictionaryElements_DictionaryElementId",
                        column: x => x.DictionaryElementId,
                        principalSchema: "lace_schema",
                        principalTable: "DictionaryElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileAttributes_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "lace_schema",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryElements_CategoryId",
                schema: "lace_schema",
                table: "DictionaryElements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAttributes_CategoryId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAttributes_DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                column: "DictionaryElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAttributes_ProfileId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                schema: "lace_schema",
                table: "Profiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileAttributes",
                schema: "lace_schema");

            migrationBuilder.DropTable(
                name: "DictionaryElements",
                schema: "lace_schema");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "lace_schema");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "lace_schema");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "lace_schema");
        }
    }
}
