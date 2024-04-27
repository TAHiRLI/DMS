using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSM.az.Migrations
{
    /// <inheritdoc />
    public partial class ServicesModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceQualification",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceQualification",
                table: "Services");
        }
    }
}
