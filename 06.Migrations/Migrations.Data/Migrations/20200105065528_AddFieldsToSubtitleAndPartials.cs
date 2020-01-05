using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class AddFieldsToSubtitleAndPartials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subtitles_EpisodeID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "RevisionDate",
                table: "SubtitlePartials");

            migrationBuilder.AddColumn<int>(
                name: "Format",
                table: "Subtitles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Subtitles",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "SubtitlePartials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SubtitlePartials",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SubtitlePartials",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_EpisodeID_FansubID",
                table: "Subtitles",
                columns: new[] { "EpisodeID", "FansubID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubtitlePartials_UserID_SubtitleID",
                table: "SubtitlePartials",
                columns: new[] { "UserID", "SubtitleID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subtitles_EpisodeID_FansubID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials");

            migrationBuilder.DropIndex(
                name: "IX_SubtitlePartials_UserID_SubtitleID",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "SubtitlePartials");

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "SubtitlePartials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevisionDate",
                table: "SubtitlePartials",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials",
                columns: new[] { "UserID", "SubtitleID" });

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_EpisodeID",
                table: "Subtitles",
                column: "EpisodeID");
        }
    }
}
