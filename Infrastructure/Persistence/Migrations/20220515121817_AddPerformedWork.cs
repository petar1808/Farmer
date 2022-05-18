using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddPerformedWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuelSum",
                table: "PerformedWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelUsed",
                table: "PerformedWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PerforemedWorkDate",
                table: "PerformedWorks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelSum",
                table: "PerformedWorks");

            migrationBuilder.DropColumn(
                name: "FuelUsed",
                table: "PerformedWorks");

            migrationBuilder.DropColumn(
                name: "PerforemedWorkDate",
                table: "PerformedWorks");
        }
    }
}
