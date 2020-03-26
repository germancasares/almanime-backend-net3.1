using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class UpdateFansubProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainLanguage",
                table: "Fansubs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MembershipOption",
                table: "Fansubs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Webpage",
                table: "Fansubs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainLanguage",
                table: "Fansubs");

            migrationBuilder.DropColumn(
                name: "MembershipOption",
                table: "Fansubs");

            migrationBuilder.DropColumn(
                name: "Webpage",
                table: "Fansubs");
        }
    }
}
