using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RepeatEveryAmount = table.Column<int>(type: "integer", nullable: false),
                    RepeatEveryTimePeriod = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProductConfigurationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedOptions_ProductConfigurations_ProductConfigurationId",
                        column: x => x.ProductConfigurationId,
                        principalTable: "ProductConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedOptions_ProductConfigurationId",
                table: "SelectedOptions",
                column: "ProductConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedOptions");

            migrationBuilder.DropTable(
                name: "ProductConfigurations");

            migrationBuilder.UpdateData(
                table: "options",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a"),
                column: "Values",
                value: new List<string> { "Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos" });
        }
    }
}
