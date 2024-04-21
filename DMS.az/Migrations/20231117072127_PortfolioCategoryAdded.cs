using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSM.az.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioCategoryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfoliCategoryId",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortfolioCategoryId",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PortfolioCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_PortfolioCategoryId",
                table: "Portfolios",
                column: "PortfolioCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_PortfolioCategories_PortfolioCategoryId",
                table: "Portfolios",
                column: "PortfolioCategoryId",
                principalTable: "PortfolioCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_PortfolioCategories_PortfolioCategoryId",
                table: "Portfolios");

            migrationBuilder.DropTable(
                name: "PortfolioCategories");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_PortfolioCategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "PortfoliCategoryId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "PortfolioCategoryId",
                table: "Portfolios");
        }
    }
}
