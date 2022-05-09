using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JuanBackFinal.Migrations
{
    public partial class Edit_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublisherImage",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublisherName",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublisherPosition",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    PublisherName = table.Column<string>(maxLength: 255, nullable: false),
                    PublisherPosition = table.Column<string>(maxLength: 255, nullable: false),
                    PublisherImage = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_PublisherId",
                table: "Blogs",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Publishers_PublisherId",
                table: "Blogs",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Publishers_PublisherId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_PublisherId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "PublisherImage",
                table: "Blogs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublisherName",
                table: "Blogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublisherPosition",
                table: "Blogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
