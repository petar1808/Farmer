using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class UpdateTreatment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Articles_ArticleId",
                table: "Treatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Seedings_SeedingId",
                table: "Treatment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treatment",
                table: "Treatment");

            migrationBuilder.RenameTable(
                name: "Treatment",
                newName: "Treatments");

            migrationBuilder.RenameIndex(
                name: "IX_Treatment_SeedingId",
                table: "Treatments",
                newName: "IX_Treatments_SeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatment_ArticleId",
                table: "Treatments",
                newName: "IX_Treatments_ArticleId");

            migrationBuilder.AddColumn<int>(
                name: "ArticlePrice",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treatments",
                table: "Treatments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Articles_ArticleId",
                table: "Treatments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Seedings_SeedingId",
                table: "Treatments",
                column: "SeedingId",
                principalTable: "Seedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Articles_ArticleId",
                table: "Treatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Seedings_SeedingId",
                table: "Treatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treatments",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "ArticlePrice",
                table: "Treatments");

            migrationBuilder.RenameTable(
                name: "Treatments",
                newName: "Treatment");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_SeedingId",
                table: "Treatment",
                newName: "IX_Treatment_SeedingId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_ArticleId",
                table: "Treatment",
                newName: "IX_Treatment_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treatment",
                table: "Treatment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Articles_ArticleId",
                table: "Treatment",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Seedings_SeedingId",
                table: "Treatment",
                column: "SeedingId",
                principalTable: "Seedings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
