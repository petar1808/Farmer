using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddTreatmentFixToAllModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformedWorks_Articles_ArticleId",
                table: "PerformedWorks");

            migrationBuilder.DropIndex(
                name: "IX_PerformedWorks_ArticleId",
                table: "PerformedWorks");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "PerformedWorks");

            migrationBuilder.RenameColumn(
                name: "PerforemedWorkDate",
                table: "PerformedWorks",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "FuelUsed",
                table: "PerformedWorks",
                newName: "FuelPrice");

            migrationBuilder.RenameColumn(
                name: "FuelSum",
                table: "PerformedWorks",
                newName: "AmountOfFuel");

            migrationBuilder.AddColumn<int>(
                name: "GrainPricePerKilogram",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HarvestedQuantityPerDecare",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityOfSeedsPerDecare",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeedPricePerKilogram",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Subsidies",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ТreatmentType = table.Column<int>(type: "int", nullable: false),
                    AmountOfFuel = table.Column<int>(type: "int", nullable: true),
                    FuelPrice = table.Column<int>(type: "int", nullable: true),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    ArticleQuantity = table.Column<int>(type: "int", nullable: false),
                    SeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatment_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Treatment_Seedings_SeedingId",
                        column: x => x.SeedingId,
                        principalTable: "Seedings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_ArticleId",
                table: "Treatment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_SeedingId",
                table: "Treatment",
                column: "SeedingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropColumn(
                name: "GrainPricePerKilogram",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "HarvestedQuantityPerDecare",
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

            migrationBuilder.RenameColumn(
                name: "FuelPrice",
                table: "PerformedWorks",
                newName: "FuelUsed");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "PerformedWorks",
                newName: "PerforemedWorkDate");

            migrationBuilder.RenameColumn(
                name: "AmountOfFuel",
                table: "PerformedWorks",
                newName: "FuelSum");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "PerformedWorks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformedWorks_ArticleId",
                table: "PerformedWorks",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformedWorks_Articles_ArticleId",
                table: "PerformedWorks",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
