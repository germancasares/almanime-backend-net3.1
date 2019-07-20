using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class Episodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Chapters_ChapterID",
                table: "Subtitles");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropColumn(
                name: "Episodes",
                table: "Animes");

            migrationBuilder.RenameColumn(
                name: "ChapterID",
                table: "Subtitles",
                newName: "EpisodeID");

            migrationBuilder.RenameIndex(
                name: "IX_Subtitles_ChapterID",
                table: "Subtitles",
                newName: "IX_Subtitles_EpisodeID");

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Aired = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    AnimeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Episodes_Animes_AnimeID",
                        column: x => x.AnimeID,
                        principalTable: "Animes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_AnimeID",
                table: "Episodes",
                column: "AnimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Episodes_EpisodeID",
                table: "Subtitles",
                column: "EpisodeID",
                principalTable: "Episodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Episodes_EpisodeID",
                table: "Subtitles");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.RenameColumn(
                name: "EpisodeID",
                table: "Subtitles",
                newName: "ChapterID");

            migrationBuilder.RenameIndex(
                name: "IX_Subtitles_EpisodeID",
                table: "Subtitles",
                newName: "IX_Subtitles_ChapterID");

            migrationBuilder.AddColumn<int>(
                name: "Episodes",
                table: "Animes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Aired = table.Column<DateTime>(nullable: true),
                    AnimeID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Duration = table.Column<int>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Chapters_Animes_AnimeID",
                        column: x => x.AnimeID,
                        principalTable: "Animes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_AnimeID",
                table: "Chapters",
                column: "AnimeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Chapters_ChapterID",
                table: "Subtitles",
                column: "ChapterID",
                principalTable: "Chapters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
