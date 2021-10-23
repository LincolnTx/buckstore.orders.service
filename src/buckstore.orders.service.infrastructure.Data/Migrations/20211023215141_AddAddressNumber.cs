using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.orders.service.infrastructure.Data.Migrations
{
    public partial class AddAddressNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Address_Number",
                table: "order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "order");
        }
    }
}
