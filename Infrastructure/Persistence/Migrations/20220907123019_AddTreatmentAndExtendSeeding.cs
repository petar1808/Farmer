using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddTreatmentAndExtendSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformedWorks_Articles_ArticleId",
                table: "PerformedWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings");

            migrationBuilder.DropIndex(
                name: "IX_PerformedWorks_ArticleId",
                table: "PerformedWorks");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "PerformedWorks",
                newName: "FuelPrice");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
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
                name: "HarvestedQuantityPerDecare",
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

            migrationBuilder.AddColumn<int>(
                name: "AmountOfFuel",
                table: "PerformedWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PerformedWorks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Treatments",
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
                    ArticlePrice = table.Column<int>(type: "int", nullable: false),
                    SeedingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Treatments_Seedings_SeedingId",
                        column: x => x.SeedingId,
                        principalTable: "Seedings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_ArticleId",
                table: "Treatments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_SeedingId",
                table: "Treatments",
                column: "SeedingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings");

            migrationBuilder.DropTable(
                name: "Treatments");

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

            migrationBuilder.DropColumn(
                name: "AmountOfFuel",
                table: "PerformedWorks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PerformedWorks");

            migrationBuilder.RenameColumn(
                name: "FuelPrice",
                table: "PerformedWorks",
                newName: "ArticleId");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Seedings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
