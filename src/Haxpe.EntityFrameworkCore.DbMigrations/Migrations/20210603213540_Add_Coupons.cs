using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Haxpe.Migrations
{
    public partial class Add_Coupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "HaxpeOrders",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                table: "HaxpeOrders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HaxpeCoupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaxpeCoupon", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HaxpeCoupon_Code",
                table: "HaxpeCoupon",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HaxpeCoupon");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "HaxpeOrders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "HaxpeOrders");
        }
    }
}
