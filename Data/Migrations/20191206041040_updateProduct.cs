using Microsoft.EntityFrameworkCore.Migrations;

namespace doan_cnpm.Data.Migrations
{
    public partial class updateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductColor",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductColor",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "Products");
        }
    }
}
