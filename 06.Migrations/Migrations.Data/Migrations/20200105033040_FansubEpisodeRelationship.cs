using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class FansubEpisodeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubtitlePartials_Memberships_MembershipID",
                table: "SubtitlePartials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "MembershipID",
                table: "SubtitlePartials");

            migrationBuilder.AddColumn<Guid>(
                name: "FansubID",
                table: "Subtitles",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "SubtitlePartials",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials",
                columns: new[] { "UserID", "SubtitleID" });

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_FansubID",
                table: "Subtitles",
                column: "FansubID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubtitlePartials_Users_UserID",
                table: "SubtitlePartials",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Fansubs_FansubID",
                table: "Subtitles",
                column: "FansubID",
                principalTable: "Fansubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubtitlePartials_Users_UserID",
                table: "SubtitlePartials");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Fansubs_FansubID",
                table: "Subtitles");

            migrationBuilder.DropIndex(
                name: "IX_Subtitles_FansubID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials");

            migrationBuilder.DropColumn(
                name: "FansubID",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "SubtitlePartials");

            migrationBuilder.AddColumn<Guid>(
                name: "MembershipID",
                table: "SubtitlePartials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubtitlePartials",
                table: "SubtitlePartials",
                columns: new[] { "MembershipID", "SubtitleID" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubtitlePartials_Memberships_MembershipID",
                table: "SubtitlePartials",
                column: "MembershipID",
                principalTable: "Memberships",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
