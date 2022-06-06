using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopsRUs.Migrations
{
    public partial class DiscountRequiredYears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "RequiredYearsForUser",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequiredYearsForUser",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredYearsForUser",
                table: "Discounts");

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "DiscountType", "Name", "UserType" },
                values: new object[] { 2, 10.0, 1, "AffiliateDiscount", 2 });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "DiscountType", "Name", "UserType" },
                values: new object[] { 3, 30.0, 1, "EmployeeDiscount", 3 });
        }
    }
}
