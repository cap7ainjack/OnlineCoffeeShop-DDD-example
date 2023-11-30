using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoffeeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ALTER_PRODUCT_CURRENCY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price_Currency",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Price_Currency",
                table: "Products",
                type: "nvarchar(3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
