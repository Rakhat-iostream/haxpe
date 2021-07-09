using Microsoft.EntityFrameworkCore.Migrations;

namespace Haxpe.Migrations
{
    public partial class Cancel_Order_Reason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "HaxpeOrders",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "HaxpeOrders");
        }
    }
}
