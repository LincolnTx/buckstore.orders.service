using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.orders.service.infrastructure.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buyers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Cpf = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Status = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BuyerId = table.Column<Guid>(nullable: false),
                    CardHolderName = table.Column<string>(maxLength: 200, nullable: false),
                    CardNumber = table.Column<string>(maxLength: 25, nullable: false),
                    Expiration = table.Column<DateTime>(maxLength: 25, nullable: false),
                    SecurityNumber = table.Column<string>(maxLength: 5, nullable: false),
                    Alias = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_methods_buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_ZipCode = table.Column<string>(nullable: true),
                    Address_District = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    OrderStatusId = table.Column<int>(nullable: false),
                    value = table.Column<decimal>(nullable: false),
                    BuyerId = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PaymentMethodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_order_status_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "order_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_payment_methods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "payment_methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    product_name = table.Column<string>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "order_status",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "Submitted" },
                    { 2, "StockConfirmation" },
                    { 3, "Pending" },
                    { 4, "Accept" },
                    { 5, "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_BuyerId",
                table: "order",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_order_OrderStatusId",
                table: "order",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_order_PaymentMethodId",
                table: "order",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_OrderId",
                table: "order_item",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_methods_BuyerId",
                table: "payment_methods",
                column: "BuyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "buyers");
        }
    }
}
