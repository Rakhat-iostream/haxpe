using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Haxpe.Migrations
{
    public partial class Add_Creation_Date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "HaxpeCoupon",
                newName: "CreationDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeWorkers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeWorkerLocationTracker",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeServiceTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpePartners",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeOrders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeIndustries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeCustomers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "HaxpeAddresses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeWorkers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeWorkerLocationTracker");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeServiceTypes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpePartners");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeOrders");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeIndustries");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeCustomers");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "HaxpeAddresses");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "HaxpeCoupon",
                newName: "CreatedDate");
        }
    }
}
