using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.orders.service.infrastructure.Data.Migrations
{
    public partial class FixOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_payment_methods_PaymentMethodId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_PaymentMethodId",
                table: "order");

            migrationBuilder.DeleteData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<Guid>(
                name: "_paymentMethodId",
                table: "order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: "StockConfirmation");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status",
                value: "Accept");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 4,
                column: "Status",
                value: "Cancelled");

            migrationBuilder.CreateIndex(
                name: "IX_order__paymentMethodId",
                table: "order",
                column: "_paymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_payment_methods__paymentMethodId",
                table: "order",
                column: "_paymentMethodId",
                principalTable: "payment_methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_payment_methods__paymentMethodId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order__paymentMethodId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "_paymentMethodId",
                table: "order");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: "Submitted");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: "StockConfirmation");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "order_status",
                keyColumn: "Id",
                keyValue: 4,
                column: "Status",
                value: "Accept");

            migrationBuilder.InsertData(
                table: "order_status",
                columns: new[] { "Id", "Status" },
                values: new object[] { 5, "Cancelled" });

            migrationBuilder.CreateIndex(
                name: "IX_order_PaymentMethodId",
                table: "order",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_payment_methods_PaymentMethodId",
                table: "order",
                column: "PaymentMethodId",
                principalTable: "payment_methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
