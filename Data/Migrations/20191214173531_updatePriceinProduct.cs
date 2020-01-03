using Microsoft.EntityFrameworkCore.Migrations;

namespace doan_cnpm.Data.Migrations
{
    public partial class updatePriceinProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNew",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "newPrice",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Appoinments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNew",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "newPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Appoinments");
        }
    }
}
