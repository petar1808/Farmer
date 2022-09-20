using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class FixSeedingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrainPricePerKilogram",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "QuantityOfSeedsPerDecare",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "SeedPricePerKilogram",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "Subsidies",
                table: "Seedings");

            migrationBuilder.AlterColumn<int>(
                name: "HarvestedQuantityPerDecare",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HarvestedGrainSellingPricePerKilogram",
                table: "Seedings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SeedsPricePerKilogram",
                table: "Seedings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SeedsQuantityPerDecare",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SubsidiesIncome",
                table: "Seedings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HarvestedGrainSellingPricePerKilogram",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "SeedsPricePerKilogram",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "SeedsQuantityPerDecare",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "SubsidiesIncome",
                table: "Seedings");

            migrationBuilder.AlterColumn<int>(
                name: "HarvestedQuantityPerDecare",
                table: "Seedings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GrainPricePerKilogram",
                table: "Seedings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityOfSeedsPerDecare",
                table: "Seedings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeedPricePerKilogram",
                table: "Seedings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subsidies",
                table: "Seedings",
                type: "int",
                nullable: true);
        }
    }
}
