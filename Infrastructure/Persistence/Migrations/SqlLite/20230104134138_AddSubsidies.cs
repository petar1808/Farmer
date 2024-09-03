using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class AddSubsidies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubsidiesIncome",
                table: "Seedings");

            migrationBuilder.CreateTable(
                name: "Subsidies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeedingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsidies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsidies_Seedings_SeedingId",
                        column: x => x.SeedingId,
                        principalTable: "Seedings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subsidies_SeedingId",
                table: "Subsidies",
                column: "SeedingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subsidies");

            migrationBuilder.AddColumn<decimal>(
                name: "SubsidiesIncome",
                table: "Seedings",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
