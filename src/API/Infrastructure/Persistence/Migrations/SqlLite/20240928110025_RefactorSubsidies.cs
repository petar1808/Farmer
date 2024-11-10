using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class RefactorSubsidies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Create a temporary table
            migrationBuilder.Sql(@"
                        CREATE TABLE TempOldSubsidy
                        (
                            Date DATE NOT NULL,
                            WorkingSeasonId INT NOT NULL,
                            ArableLandId INT,
                            TenantId INT NOT NULL,
                            Income DECIMAL(18, 2) NOT NULL
                        );
                    ");

            // Step 2: Insert data into the temporary table
            migrationBuilder.Sql(@"
                        INSERT INTO TempOldSubsidy (Date, WorkingSeasonId, ArableLandId, TenantId, Income)
                        SELECT 
                            s.Date,
                            s2.WorkingSeasonId,
                            s2.ArableLandId,
                            s.TenantId,
                            s.Income
                        FROM Subsidies s
                        JOIN Seedings s2 ON s2.Id = s.SeedingId;
                    ");

            migrationBuilder.DropForeignKey(
                name: "FK_Subsidies_Seedings_SeedingId",
                table: "Subsidies");

            migrationBuilder.DropIndex(
                name: "IX_Subsidies_SeedingId",
                table: "Subsidies");

            // Step 3: Delete from Subsidy
            migrationBuilder.Sql(@"DELETE FROM Subsidies");

            migrationBuilder.RenameColumn(
                name: "SeedingId",
                table: "Subsidies",
                newName: "WorkingSeasonId");

            // Step 4: Insert aggregated data into the new Subsidy table
            migrationBuilder.Sql(@"
                        INSERT INTO Subsidies (Date, WorkingSeasonId, TenantId, Income)
                        SELECT 
                            Date(t.Date),
                            t.WorkingSeasonId,
                            t.TenantId,
                            SUM(t.Income)
                        FROM TempOldSubsidy t
                        GROUP BY Date(t.Date), t.WorkingSeasonId, t.TenantId;
                    ");

            migrationBuilder.CreateTable(
                name: "SubsidyByArableLand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArableLandId = table.Column<int>(type: "INTEGER", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    SubsidyId = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidyByArableLand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsidyByArableLand_ArableLands_ArableLandId",
                        column: x => x.ArableLandId,
                        principalTable: "ArableLands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubsidyByArableLand_Subsidies_SubsidyId",
                        column: x => x.SubsidyId,
                        principalTable: "Subsidies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyByArableLand_ArableLandId",
                table: "SubsidyByArableLand",
                column: "ArableLandId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyByArableLand_SubsidyId",
                table: "SubsidyByArableLand",
                column: "SubsidyId");

            // Step 5: Insert detailed data into the SubsidyByArableLand table
            migrationBuilder.Sql(@"
                    INSERT INTO SubsidyByArableLand (SubsidyId, ArableLandId, Income, TenantId)
                    SELECT 
                        s.Id,
                        t.ArableLandId,
                        t.Income,
                        t.TenantId
                    FROM TempOldSubsidy t
                    JOIN Subsidies s ON Date(s.Date) = Date(t.Date) AND s.WorkingSeasonId = t.WorkingSeasonId; 
                ");

            // Step 6: Drop the temporary table
            migrationBuilder.Sql(@"DROP TABLE TempOldSubsidy;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubsidyByArableLand");

            migrationBuilder.RenameColumn(
                name: "WorkingSeasonId",
                table: "Subsidies",
                newName: "SeedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidies_SeedingId",
                table: "Subsidies",
                column: "SeedingId");

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
