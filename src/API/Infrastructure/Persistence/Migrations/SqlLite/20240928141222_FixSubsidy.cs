using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class FixSubsidy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLand_ArableLands_ArableLandId",
                table: "SubsidyByArableLand");

            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLand_Subsidies_SubsidyId",
                table: "SubsidyByArableLand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubsidyByArableLand",
                table: "SubsidyByArableLand");

            migrationBuilder.RenameTable(
                name: "SubsidyByArableLand",
                newName: "SubsidyByArableLands");

            migrationBuilder.RenameIndex(
                name: "IX_SubsidyByArableLand_SubsidyId",
                table: "SubsidyByArableLands",
                newName: "IX_SubsidyByArableLands_SubsidyId");

            migrationBuilder.RenameIndex(
                name: "IX_SubsidyByArableLand_ArableLandId",
                table: "SubsidyByArableLands",
                newName: "IX_SubsidyByArableLands_ArableLandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubsidyByArableLands",
                table: "SubsidyByArableLands",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidies_WorkingSeasonId",
                table: "Subsidies",
                column: "WorkingSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsidies_WorkingSeasons_WorkingSeasonId",
                table: "Subsidies",
                column: "WorkingSeasonId",
                principalTable: "WorkingSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLands_ArableLands_ArableLandId",
                table: "SubsidyByArableLands",
                column: "ArableLandId",
                principalTable: "ArableLands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands",
                column: "SubsidyId",
                principalTable: "Subsidies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsidies_WorkingSeasons_WorkingSeasonId",
                table: "Subsidies");

            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLands_ArableLands_ArableLandId",
                table: "SubsidyByArableLands");

            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands");

            migrationBuilder.DropIndex(
                name: "IX_Subsidies_WorkingSeasonId",
                table: "Subsidies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubsidyByArableLands",
                table: "SubsidyByArableLands");

            migrationBuilder.RenameTable(
                name: "SubsidyByArableLands",
                newName: "SubsidyByArableLand");

            migrationBuilder.RenameIndex(
                name: "IX_SubsidyByArableLands_SubsidyId",
                table: "SubsidyByArableLand",
                newName: "IX_SubsidyByArableLand_SubsidyId");

            migrationBuilder.RenameIndex(
                name: "IX_SubsidyByArableLands_ArableLandId",
                table: "SubsidyByArableLand",
                newName: "IX_SubsidyByArableLand_ArableLandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubsidyByArableLand",
                table: "SubsidyByArableLand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLand_ArableLands_ArableLandId",
                table: "SubsidyByArableLand",
                column: "ArableLandId",
                principalTable: "ArableLands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLand_Subsidies_SubsidyId",
                table: "SubsidyByArableLand",
                column: "SubsidyId",
                principalTable: "Subsidies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
