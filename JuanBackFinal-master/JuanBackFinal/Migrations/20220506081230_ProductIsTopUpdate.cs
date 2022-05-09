using Microsoft.EntityFrameworkCore.Migrations;

namespace JuanBackFinal.Migrations
{
    public partial class ProductIsTopUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TopSeller",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopSeller",
                table: "Products");
        }
    }
}
