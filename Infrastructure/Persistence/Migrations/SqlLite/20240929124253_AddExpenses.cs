using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class AddExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkingSeasonId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "ExpenseByArableLand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArableLandId = table.Column<int>(type: "INTEGER", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseByArableLand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseByArableLand_ArableLands_ArableLandId",
                        column: x => x.ArableLandId,
                        principalTable: "ArableLands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseByArableLand_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseByArableLand_ArableLandId",
                table: "ExpenseByArableLand",
                column: "ArableLandId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseByArableLand_ExpenseId",
                table: "ExpenseByArableLand",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ArticleId",
                table: "Expenses",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_WorkingSeasonId",
                table: "Expenses",
                column: "WorkingSeasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseByArableLand");

            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
