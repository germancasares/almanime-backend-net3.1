using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Migrations.Data.Migrations
{
    public partial class InitialCommitWithAnime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    KitsuID = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Season = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Synopsis = table.Column<string>(nullable: true),
                    Episodes = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animes_KitsuID",
                table: "Animes",
                column: "KitsuID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animes");
        }
    }
}
