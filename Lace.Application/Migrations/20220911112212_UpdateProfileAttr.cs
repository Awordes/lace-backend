using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lace.Application.Migrations
{
    public partial class UpdateProfileAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileAttributes_DictionaryElements_DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileAttributes_DictionaryElements_DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                column: "DictionaryElementId",
                principalSchema: "lace_schema",
                principalTable: "DictionaryElements",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileAttributes_DictionaryElements_DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileAttributes_DictionaryElements_DictionaryElementId",
                schema: "lace_schema",
                table: "ProfileAttributes",
                column: "DictionaryElementId",
                principalSchema: "lace_schema",
                principalTable: "DictionaryElements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
