using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSM.az.Migrations
{
    /// <inheritdoc />
    public partial class MessageModified_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderPhoneNumber",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderPhoneNumber",
                table: "Messages");
        }
    }
}
