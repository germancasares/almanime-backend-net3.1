using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class AnimeImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Animes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Animes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterImageUrl",
                table: "Animes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "PosterImageUrl",
                table: "Animes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Animes",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
