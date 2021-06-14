using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Haxpe.Migrations
{
    public partial class Add_Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HaxpeFileInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    FileType = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaxpeFileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HaxpePartnerFileInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaxpePartnerFileInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HaxpePartnerFileInfos_PartnerId",
                table: "HaxpePartnerFileInfos",
                column: "PartnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HaxpeFileInfos");

            migrationBuilder.DropTable(
                name: "HaxpePartnerFileInfos");
        }
    }
}
