using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB.Migrations
{
    /// <inheritdoc />
    public partial class InitialDomain_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Orders_OrderId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReviewId",
                table: "Orders",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Reviews_ReviewId",
                table: "Orders",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Reviews_ReviewId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReviewId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Orders_OrderId",
                table: "Reviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
