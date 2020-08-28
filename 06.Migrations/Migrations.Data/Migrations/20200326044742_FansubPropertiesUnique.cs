using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class FansubPropertiesUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Fansubs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Acronym",
                table: "Fansubs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fansubs_Acronym",
                table: "Fansubs",
                column: "Acronym",
                unique: true,
                filter: "[Acronym] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fansubs_FullName",
                table: "Fansubs",
                column: "FullName",
                unique: true,
                filter: "[FullName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fansubs_Acronym",
                table: "Fansubs");

            migrationBuilder.DropIndex(
                name: "IX_Fansubs_FullName",
                table: "Fansubs");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Fansubs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Acronym",
                table: "Fansubs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
