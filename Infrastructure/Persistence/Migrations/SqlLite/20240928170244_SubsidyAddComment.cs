using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class SubsidyAddComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Subsidies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands",
                column: "SubsidyId",
                principalTable: "Subsidies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Subsidies");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsidyByArableLands_Subsidies_SubsidyId",
                table: "SubsidyByArableLands",
                column: "SubsidyId",
                principalTable: "Subsidies",
                principalColumn: "Id");
        }
    }
}
