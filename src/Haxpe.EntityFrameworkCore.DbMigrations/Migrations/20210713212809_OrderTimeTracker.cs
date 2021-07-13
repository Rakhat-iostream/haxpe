using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Haxpe.Migrations
{
    public partial class OrderTimeTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HaxpeOrderTimeTrackers",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaxpeOrderTimeTrackers", x => new { x.OrderId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_HaxpeOrderTimeTrackers_HaxpeOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "HaxpeOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HaxpeOrderTimeTrackers");
        }
    }
}
