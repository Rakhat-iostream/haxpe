using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Haxpe.Migrations
{
    public partial class Add_PartnersIndustry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HaxpePartnersIndustries",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IndustryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaxpePartnersIndustries", x => new { x.PartnerId, x.IndustryId });
                    table.ForeignKey(
                        name: "FK_HaxpePartnersIndustries_HaxpePartners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "HaxpePartners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HaxpePartnersIndustries");
        }
    }
}
