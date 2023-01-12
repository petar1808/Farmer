using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlLite
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingSeasons_Name",
                table: "WorkingSeasons");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSeasons_Name_TenantId",
                table: "WorkingSeasons",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Name_TenantId_ArticleType",
                table: "Articles",
                columns: new[] { "Name", "TenantId", "ArticleType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArableLands_Name_TenantId",
                table: "ArableLands",
                columns: new[] { "Name", "TenantId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingSeasons_Name_TenantId",
                table: "WorkingSeasons");

            migrationBuilder.DropIndex(
                name: "IX_Articles_Name_TenantId_ArticleType",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_ArableLands_Name_TenantId",
                table: "ArableLands");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSeasons_Name",
                table: "WorkingSeasons",
                column: "Name",
                unique: true);
        }
    }
}
