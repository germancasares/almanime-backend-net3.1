using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Data.Migrations
{
    public partial class Subtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Membership_Fansubs_FansubID",
                table: "Membership");

            migrationBuilder.DropForeignKey(
                name: "FK_Membership_Users_UserID",
                table: "Membership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.RenameTable(
                name: "Membership",
                newName: "Memberships");

            migrationBuilder.RenameIndex(
                name: "IX_Membership_UserID",
                table: "Memberships",
                newName: "IX_Memberships_UserID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Users",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Users",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Fansubs",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Fansubs",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Animes",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Animes",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Memberships",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Memberships",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Memberships",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Chapters",
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
                    table.PrimaryKey("PK_Chapters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Chapters_Animes_AnimeID",
                        column: x => x.AnimeID,
                        principalTable: "Animes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subtitles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ChapterID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subtitles_Chapters_ChapterID",
                        column: x => x.ChapterID,
                        principalTable: "Chapters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubtitlePartials",
                columns: table => new
                {
                    SubtitleID = table.Column<Guid>(nullable: false),
                    MembershipID = table.Column<Guid>(nullable: false),
                    Revision = table.Column<int>(nullable: false),
                    RevisionDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtitlePartials", x => new { x.MembershipID, x.SubtitleID });
                    table.ForeignKey(
                        name: "FK_SubtitlePartials_Memberships_MembershipID",
                        column: x => x.MembershipID,
                        principalTable: "Memberships",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubtitlePartials_Subtitles_SubtitleID",
                        column: x => x.SubtitleID,
                        principalTable: "Subtitles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityID",
                table: "Users",
                column: "IdentityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_FansubID_UserID",
                table: "Memberships",
                columns: new[] { "FansubID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_AnimeID",
                table: "Chapters",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_SubtitlePartials_SubtitleID",
                table: "SubtitlePartials",
                column: "SubtitleID");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_ChapterID",
                table: "Subtitles",
                column: "ChapterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Fansubs_FansubID",
                table: "Memberships",
                column: "FansubID",
                principalTable: "Fansubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Users_UserID",
                table: "Memberships",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Fansubs_FansubID",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Users_UserID",
                table: "Memberships");

            migrationBuilder.DropTable(
                name: "SubtitlePartials");

            migrationBuilder.DropTable(
                name: "Subtitles");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdentityID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_FansubID_UserID",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Memberships");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "Membership");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_UserID",
                table: "Membership",
                newName: "IX_Membership_UserID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Fansubs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Fansubs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Animes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Animes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                columns: new[] { "FansubID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_Fansubs_FansubID",
                table: "Membership",
                column: "FansubID",
                principalTable: "Fansubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_Users_UserID",
                table: "Membership",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
