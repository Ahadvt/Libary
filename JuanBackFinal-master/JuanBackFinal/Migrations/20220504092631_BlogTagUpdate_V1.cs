using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JuanBackFinal.Migrations
{
    public partial class BlogTagUpdate_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BlogTags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogTags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BlogTags",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BlogTags");
        }
    }
}
