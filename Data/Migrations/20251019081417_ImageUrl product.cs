using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.MigrationsIdentity
{
    /// <inheritdoc />
    public partial class ImageUrlproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("393d1534-3f8e-4b47-bdae-ed974c609982"),
                column: "ImageUrl",
                value: "https://cdn.shopify.com/s/files/1/0254/4667/8590/files/nw_WheyMilkshake_Vanille_still_final_v1_0b704c6c-7f7b-484d-bb65-7d12e1c7834a.jpg");

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });
        }
    }
}
