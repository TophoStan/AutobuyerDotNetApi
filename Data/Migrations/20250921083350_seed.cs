using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "Id", "BaseUrl", "LogoUrl", "MinimumFreeDeliveryPrice", "Name" },
                values: new object[] { new Guid("4542dfea-86e4-4fce-9446-c1b840fbefa0"), "https://www.upfront.nl/", "https://upfront.nl/cdn/shop/files/LOGO-INVOICE.svg?v=1687087217&width=80", 49.0, "Upfront" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandEntityId", "Description", "Name", "Price", "Url" },
                values: new object[] { new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"), new Guid("4542dfea-86e4-4fce-9446-c1b840fbefa0"), "Proteine enzo", "Why protein", 24m, "https://upfront.nl/products/whey" });

            migrationBuilder.InsertData(
                table: "options",
                columns: new[] { "Id", "Name", "ProductEntityId", "Values" },
                values: new object[] { new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"), "Whey_flavour", new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"), new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" } });

            migrationBuilder.InsertData(
                table: "order_steps",
                columns: new[] { "Id", "ProductEntityId", "StepInJs", "StepName" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"), "await page.click('xo-toggle-trigger[xo-name=\"variant-flavour-toggle-main-product\"]')", "Open smaken" },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"), "await page\n            .locator('div.variant-flavour-selector__popover-inner')\n            .locator('span')\n            .filter({ hasText: 'Vanille' })\n            .first()\n            .click();", "Klik op smaak" },
                    { new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"), "await page.getByText('Voeg toe').first().click();", "Voeg toe aan mandje" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"));

            migrationBuilder.DeleteData(
                table: "order_steps",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));

            migrationBuilder.DeleteData(
                table: "order_steps",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"));

            migrationBuilder.DeleteData(
                table: "order_steps",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"));

            migrationBuilder.DeleteData(
                table: "brands",
                keyColumn: "Id",
                keyValue: new Guid("4542dfea-86e4-4fce-9446-c1b840fbefa0"));
        }
    }
}
