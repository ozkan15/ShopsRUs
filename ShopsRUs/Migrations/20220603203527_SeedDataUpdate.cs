using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopsRUs.Migrations
{
    public partial class SeedDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountPercentage",
                value: 5.0);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DiscountPercentage",
                value: 10.0);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "DiscountPercentage",
                value: 30.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountPercentage",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DiscountPercentage",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "DiscountPercentage",
                value: 0.0);
        }
    }
}
