using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductIdOnConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductConfigurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_ProductId",
                table: "ProductConfigurations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfigurations_Products_ProductId",
                table: "ProductConfigurations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfigurations_Products_ProductId",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ProductConfigurations_ProductId",
                table: "ProductConfigurations");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductConfigurations");

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });
        }
    }
}
