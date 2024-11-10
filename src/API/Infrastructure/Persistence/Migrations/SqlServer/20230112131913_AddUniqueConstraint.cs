using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.SqlServer
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingSeasons_Name",
                table: "WorkingSeasons");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkingSeasons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSeasons_Name_TenantId",
                table: "WorkingSeasons",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Name",
                table: "Tenants",
                column: "Name",
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
                name: "IX_Tenants_Name",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Articles_Name_TenantId_ArticleType",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_ArableLands_Name_TenantId",
                table: "ArableLands");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkingSeasons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSeasons_Name",
                table: "WorkingSeasons",
                column: "Name",
                unique: true);
        }
    }
}
