using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class SeedingRemoveUnusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpensesForHarvesting",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "SeedsPricePerKilogram",
                table: "Seedings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExpensesForHarvesting",
                table: "Seedings",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SeedsPricePerKilogram",
                table: "Seedings",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
