using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class optionsforselected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OptionId",
                table: "SelectedOptions",
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
                name: "IX_SelectedOptions_OptionId",
                table: "SelectedOptions",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedOptions_options_OptionId",
                table: "SelectedOptions",
                column: "OptionId",
                principalTable: "options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedOptions_options_OptionId",
                table: "SelectedOptions");

            migrationBuilder.DropIndex(
                name: "IX_SelectedOptions_OptionId",
                table: "SelectedOptions");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "SelectedOptions");

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });
        }
    }
}
