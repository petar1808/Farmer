using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class RemoveUnusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfFuel",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "ArticlePrice",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "FuelPrice",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "AmountOfFuel",
                table: "PerformedWorks");

            migrationBuilder.DropColumn(
                name: "FuelPrice",
                table: "PerformedWorks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfFuel",
                table: "Treatments",
                type: "decimal(12,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ArticlePrice",
                table: "Treatments",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FuelPrice",
                table: "Treatments",
                type: "decimal(12,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfFuel",
                table: "PerformedWorks",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FuelPrice",
                table: "PerformedWorks",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
