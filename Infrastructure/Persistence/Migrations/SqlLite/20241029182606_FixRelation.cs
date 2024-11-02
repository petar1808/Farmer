using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class FixRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseByArableLand",
                table: "ExpenseByArableLand");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseByArableLand_Expenses_ExpenseId",
                table: "ExpenseByArableLand");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseByArableLand_ArableLands_ArableLandId",
                table: "ExpenseByArableLand");

            migrationBuilder.RenameTable(
                name: "ExpenseByArableLand",
                newName: "ExpenseByArableLands");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseByArableLand_ExpenseId",
                table: "ExpenseByArableLands",
                newName: "IX_ExpenseByArableLands_ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseByArableLand_ArableLandId",
                table: "ExpenseByArableLands",
                newName: "IX_ExpenseByArableLands_ArableLandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseByArableLands",
                table: "ExpenseByArableLands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseByArableLands_ArableLands_ArableLandId",
                table: "ExpenseByArableLands",
                column: "ArableLandId",
                principalTable: "ArableLands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseByArableLands_Expenses_ExpenseId",
                table: "ExpenseByArableLands",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseByArableLands_ArableLands_ArableLandId",
                table: "ExpenseByArableLands");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseByArableLands_Expenses_ExpenseId",
                table: "ExpenseByArableLands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseByArableLands",
                table: "ExpenseByArableLands");

            migrationBuilder.RenameTable(
                name: "ExpenseByArableLands",
                newName: "ExpenseByArableLand");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseByArableLands_ExpenseId",
                table: "ExpenseByArableLand",
                newName: "IX_ExpenseByArableLand_ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseByArableLands_ArableLandId",
                table: "ExpenseByArableLand",
                newName: "IX_ExpenseByArableLand_ArableLandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseByArableLand",
                table: "ExpenseByArableLand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseByArableLand_ArableLands_ArableLandId",
                table: "ExpenseByArableLand",
                column: "ArableLandId",
                principalTable: "ArableLands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseByArableLand_Expenses_ExpenseId",
                table: "ExpenseByArableLand",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
