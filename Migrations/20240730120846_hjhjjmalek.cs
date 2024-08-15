using Microsoft.EntityFrameworkCore.Migrations;

namespace Storee.Migrations
{
    public partial class hjhjjmalek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "HomeId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Home",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    arTitle = table.Column<string>(nullable: true),
                    enTitle = table.Column<string>(nullable: true),
                    arLargeTitle = table.Column<string>(nullable: true),
                    enLargeTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_HomeId",
                table: "Product",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Home_HomeId",
                table: "Product",
                column: "HomeId",
                principalTable: "Home",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Home_HomeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Home");

            migrationBuilder.DropIndex(
                name: "IX_Product_HomeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Product");
        }
    }
}
