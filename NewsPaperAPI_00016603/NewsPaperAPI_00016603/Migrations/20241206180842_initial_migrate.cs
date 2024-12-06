using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPaperAPI00016603.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsDbSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsDbSet_CategoryDbSet_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsDbSet_CategoryId",
                table: "NewsDbSet",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsDbSet");

            migrationBuilder.DropTable(
                name: "CategoryDbSet");
        }
    }
}
