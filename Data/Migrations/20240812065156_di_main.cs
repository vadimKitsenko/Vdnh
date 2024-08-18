using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class di_main : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MainId",
                table: "di_about",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "di_main",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImgId = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    user_create = table.Column<string>(type: "text", nullable: false),
                    date_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_update = table.Column<string>(type: "text", nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_di_main", x => x.id);
                    table.ForeignKey(
                        name: "FK_di_main_di_image_ImgId",
                        column: x => x.ImgId,
                        principalTable: "di_image",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_di_about_MainId",
                table: "di_about",
                column: "MainId");

            migrationBuilder.CreateIndex(
                name: "IX_di_main_ImgId",
                table: "di_main",
                column: "ImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_di_about_di_main_MainId",
                table: "di_about",
                column: "MainId",
                principalTable: "di_main",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_di_about_di_main_MainId",
                table: "di_about");

            migrationBuilder.DropTable(
                name: "di_main");

            migrationBuilder.DropIndex(
                name: "IX_di_about_MainId",
                table: "di_about");

            migrationBuilder.DropColumn(
                name: "MainId",
                table: "di_about");
        }
    }
}
