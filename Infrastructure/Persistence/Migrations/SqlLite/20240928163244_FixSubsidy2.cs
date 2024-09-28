using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class FixSubsidy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands",
                column: "SubsidyId",
                principalTable: "Subsidies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands",
                column: "SubsidyId",
                principalTable: "Subsidies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
