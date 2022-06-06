using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopsRUs.Migrations
{
    public partial class DiscountYearsEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "DiscountType", "Name", "RequiredYearsForUser", "UserType" },
                values: new object[] { 2, 10.0, 1, "AffiliateDiscount", 0, 2 });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "DiscountType", "Name", "RequiredYearsForUser", "UserType" },
                values: new object[] { 3, 30.0, 1, "EmployeeDiscount", 0, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
