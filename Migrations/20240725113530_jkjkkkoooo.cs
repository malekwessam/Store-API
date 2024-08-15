using Microsoft.EntityFrameworkCore.Migrations;

namespace Storee.Migrations
{
    public partial class jkjkkkoooo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "ShoppingCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "ShoppingCart");
        }
    }
}
