using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P132Pustok.Migrations
{
    public partial class CountAddedIntoBasketItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "BasketItems");
        }
    }
}
