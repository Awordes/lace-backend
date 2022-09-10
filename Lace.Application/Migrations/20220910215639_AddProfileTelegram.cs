using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lace.Application.Migrations
{
    public partial class AddProfileTelegram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "lace_schema",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                schema: "lace_schema",
                table: "Profiles",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telegram",
                schema: "lace_schema",
                table: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "lace_schema",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
