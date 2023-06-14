using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPISampleApp.Migrations
{
    public partial class FixRelationshipIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_CategoryCatId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CategoryCatId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryCatId",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryCatId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryCatId",
                table: "Category",
                column: "CategoryCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_CategoryCatId",
                table: "Category",
                column: "CategoryCatId",
                principalTable: "Category",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
