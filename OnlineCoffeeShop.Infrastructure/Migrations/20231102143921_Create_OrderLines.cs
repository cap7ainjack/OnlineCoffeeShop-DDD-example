using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoffeeShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_OrderLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLine_Orders_OrderId",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLine",
                table: "OrderLine");

            migrationBuilder.RenameTable(
                name: "OrderLine",
                newName: "OrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLine_OrderId",
                table: "OrderLines",
                newName: "IX_OrderLines_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines");

            migrationBuilder.RenameTable(
                name: "OrderLines",
                newName: "OrderLine");

            migrationBuilder.RenameIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLine",
                newName: "IX_OrderLine_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLine",
                table: "OrderLine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLine_Orders_OrderId",
                table: "OrderLine",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
