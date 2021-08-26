using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DentalShop.Migrations
{
    public partial class test5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductOrdersUserOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductOrderId = table.Column<int>(type: "int", nullable: false),
                    UserOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrdersUserOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrdersUserOrders_PorductOrders_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "PorductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrdersUserOrders_UserOrders_UserOrderId",
                        column: x => x.UserOrderId,
                        principalTable: "UserOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrdersUserOrders_ProductOrderId",
                table: "ProductOrdersUserOrders",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrdersUserOrders_UserOrderId",
                table: "ProductOrdersUserOrders",
                column: "UserOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrdersUserOrders");
        }
    }
}
