using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlServer
{
    public partial class AddExpenseSubsidiesAndMoreSmallFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsidies_Seedings_SeedingId",
                table: "Subsidies");

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
                name: "ExpensesForHarvesting",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "SeedsPricePerKilogram",
                table: "Seedings");

            migrationBuilder.DropColumn(
                name: "AmountOfFuel",
                table: "PerformedWorks");

            migrationBuilder.DropColumn(
                name: "FuelPrice",
                table: "PerformedWorks");

            migrationBuilder.RenameColumn(
                name: "SeedingId",
                table: "Subsidies",
                newName: "WorkingSeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Subsidies_SeedingId",
                table: "Subsidies",
                newName: "IX_Subsidies_WorkingSeasonId");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Subsidies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    WorkingSeasonId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_WorkingSeasons_WorkingSeasonId",
                        column: x => x.WorkingSeasonId,
                        principalTable: "WorkingSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubsidyByArableLands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArableLandId = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    SubsidyId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidyByArableLands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsidyByArableLands_ArableLands_ArableLandId",
                        column: x => x.ArableLandId,
                        principalTable: "ArableLands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                        column: x => x.SubsidyId,
                        principalTable: "Subsidies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseByArableLands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArableLandId = table.Column<int>(type: "int", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseByArableLands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseByArableLands_ArableLands_ArableLandId",
                        column: x => x.ArableLandId,
                        principalTable: "ArableLands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseByArableLands_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseByArableLands_ArableLandId",
                table: "ExpenseByArableLands",
                column: "ArableLandId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseByArableLands_ExpenseId",
                table: "ExpenseByArableLands",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ArticleId",
                table: "Expenses",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_WorkingSeasonId",
                table: "Expenses",
                column: "WorkingSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyByArableLands_ArableLandId",
                table: "SubsidyByArableLands",
                column: "ArableLandId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyByArableLands_SubsidyId",
                table: "SubsidyByArableLands",
                column: "SubsidyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsidies_WorkingSeasons_WorkingSeasonId",
                table: "Subsidies",
                column: "WorkingSeasonId",
                principalTable: "WorkingSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsidies_WorkingSeasons_WorkingSeasonId",
                table: "Subsidies");

            migrationBuilder.DropTable(
                name: "ExpenseByArableLands");

            migrationBuilder.DropTable(
                name: "SubsidyByArableLands");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Subsidies");

            migrationBuilder.RenameColumn(
                name: "WorkingSeasonId",
                table: "Subsidies",
                newName: "SeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_Subsidies_WorkingSeasonId",
                table: "Subsidies",
                newName: "IX_Subsidies_SeedingId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subsidies_Seedings_SeedingId",
                table: "Subsidies",
                column: "SeedingId",
                principalTable: "Seedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
