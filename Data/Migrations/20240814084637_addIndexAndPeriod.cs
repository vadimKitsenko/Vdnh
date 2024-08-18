using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addIndexAndPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "groupId",
                table: "di_vertical",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "periodId",
                table: "di_vertical",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "index",
                table: "di_horisontal",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "groupId",
                table: "di_vertical");

            migrationBuilder.DropColumn(
                name: "periodId",
                table: "di_vertical");

            migrationBuilder.DropColumn(
                name: "index",
                table: "di_horisontal");
        }
    }
}
