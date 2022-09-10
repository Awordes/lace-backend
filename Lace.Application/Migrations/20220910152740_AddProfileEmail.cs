using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lace.Application.Migrations
{
    public partial class AddProfileEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "lace_schema",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "lace_schema",
                table: "Profiles");
        }
    }
}
