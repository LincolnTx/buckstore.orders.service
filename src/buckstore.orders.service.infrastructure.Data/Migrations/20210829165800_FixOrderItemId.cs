using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.orders.service.infrastructure.Data.Migrations
{
    public partial class FixOrderItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "product_id",
                table: "order_item",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_id",
                table: "order_item");
        }
    }
}
