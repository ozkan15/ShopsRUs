using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopsRUs.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPercentage = table.Column<double>(type: "float", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    AffiliateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PercentageDiscountExcludedProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PercentageDiscountExcludedProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PercentageDiscountExcludedProductCategories_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PercentageDiscountExcludedProductCategories_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AmountNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmoutTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_ShopUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ShopUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountInvoice",
                columns: table => new
                {
                    DiscountsId = table.Column<int>(type: "int", nullable: false),
                    InvoicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountInvoice", x => new { x.DiscountsId, x.InvoicesId });
                    table.ForeignKey(
                        name: "FK_DiscountInvoice_Discounts_DiscountsId",
                        column: x => x.DiscountsId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountInvoice_Invoices_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountAmount", "DiscountType", "DiscountableAmount", "Name" },
                values: new object[] { 4, 5m, 2, 100m, "5DollarDiscount" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "DiscountType", "Name", "UserType" },
                values: new object[,]
                {
                    { 1, 0.0, 1, "CustomerDiscount", 1 },
                    { 2, 0.0, 1, "AffiliateDiscount", 2 },
                    { 3, 0.0, 1, "EmployeeDiscount", 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Groceries" },
                    { 2, "Computer" },
                    { 3, "Electronics" },
                    { 4, "Video Games" }
                });

            migrationBuilder.InsertData(
                table: "ShopUser",
                columns: new[] { "Id", "Address", "AffiliateName", "Name", "RegistrationDate", "Surname", "UserType" },
                values: new object[] { 2, "Avda. de la Constitución 2222", "Amazon", "Ana", new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trujillo", 2 });

            migrationBuilder.InsertData(
                table: "ShopUser",
                columns: new[] { "Id", "Address", "Name", "RegistrationDate", "Surname", "UserType" },
                values: new object[,]
                {
                    { 1, "Obere Str. 57", "Maria", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anders", 1 },
                    { 4, "120 Hanover Sq.", "Thomas", new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardy", 1 }
                });

            migrationBuilder.InsertData(
                table: "ShopUser",
                columns: new[] { "Id", "Address", "Name", "RegistrationDate", "Surname", "Title", "UserType" },
                values: new object[] { 3, "Mataderos  2312", "Antonio", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Moreno", "Sales Representative", 3 });

            migrationBuilder.InsertData(
                table: "PercentageDiscountExcludedProductCategories",
                columns: new[] { "Id", "DiscountId", "ProductCategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "ItemName", "Price" },
                values: new object[,]
                {
                    { 1, 3, "Apple AirPods (2nd Generation)", 99.99m },
                    { 2, 3, "HP DeskJet 2755e", 112.50m },
                    { 3, 3, "Seagate Portable 2TB External Hard Drive", 61.99m },
                    { 4, 2, "Acer Aspire 5", 391.00m },
                    { 5, 2, "Lenovo Flex 5 Laptop", 671.00m },
                    { 6, 2, "Acer Nitro 5", 669.99m },
                    { 7, 4, "Pokémon Legends", 53.85m },
                    { 8, 4, "Grand Theft Auto V", 34.99m },
                    { 9, 1, "Oreo Cookies", 5.98m },
                    { 10, 1, "Bread", 1.10m },
                    { 11, 1, "Doritos 3D Crunch", 3.48m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountInvoice_InvoicesId",
                table: "DiscountInvoice",
                column: "InvoicesId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PercentageDiscountExcludedProductCategories_DiscountId",
                table: "PercentageDiscountExcludedProductCategories",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_PercentageDiscountExcludedProductCategories_ProductCategoryId",
                table: "PercentageDiscountExcludedProductCategories",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountInvoice");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "PercentageDiscountExcludedProductCategories");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "ShopUser");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
