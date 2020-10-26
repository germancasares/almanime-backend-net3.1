using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class RequirednessOnFansub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fansubs_FullName",
                table: "Fansubs");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Fansubs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fansubs_FullName",
                table: "Fansubs",
                column: "FullName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fansubs_FullName",
                table: "Fansubs");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Fansubs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Fansubs_FullName",
                table: "Fansubs",
                column: "FullName",
                unique: true,
                filter: "[FullName] IS NOT NULL");
        }
    }
}
