using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updateimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "listPlace",
                table: "di_image",
                newName: "priority");

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "di_image",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                table: "di_image");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "di_image",
                newName: "listPlace");
        }
    }
}
