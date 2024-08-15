using Microsoft.EntityFrameworkCore.Migrations;

namespace Storee.Migrations
{
    public partial class kjjjlnnnnnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "arCurrencyName",
                table: "ProductPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "enCurrencyName",
                table: "ProductPrice",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSale",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePercentage",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arCurrencyName",
                table: "ProductPrice");

            migrationBuilder.DropColumn(
                name: "enCurrencyName",
                table: "ProductPrice");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsSale",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalePercentage",
                table: "Product");
        }
    }
}
