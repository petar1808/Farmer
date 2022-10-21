using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddDeleteBehaviorRestrictInSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_ArableLands_ArableLandId",
                table: "Seedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_WorkingSeasons_WorkingSeasonId",
                table: "Seedings");

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_ArableLands_ArableLandId",
                table: "Seedings",
                column: "ArableLandId",
                principalTable: "ArableLands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_WorkingSeasons_WorkingSeasonId",
                table: "Seedings",
                column: "WorkingSeasonId",
                principalTable: "WorkingSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_ArableLands_ArableLandId",
                table: "Seedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seedings_WorkingSeasons_WorkingSeasonId",
                table: "Seedings");

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_ArableLands_ArableLandId",
                table: "Seedings",
                column: "ArableLandId",
                principalTable: "ArableLands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_Articles_ArticleId",
                table: "Seedings",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seedings_WorkingSeasons_WorkingSeasonId",
                table: "Seedings",
                column: "WorkingSeasonId",
                principalTable: "WorkingSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
