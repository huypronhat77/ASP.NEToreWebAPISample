using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPISampleApp.Migrations
{
    public partial class AddCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryCatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CatId);
                    table.ForeignKey(
                        name: "FK_Category_Category_CategoryCatId",
                        column: x => x.CategoryCatId,
                        principalTable: "Category",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_CatId",
                table: "products",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CategoryCatId",
                table: "Category",
                column: "CategoryCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Category_CatId",
                table: "products",
                column: "CatId",
                principalTable: "Category",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Category_CatId",
                table: "products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_products_CatId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "products");
        }
    }
}
