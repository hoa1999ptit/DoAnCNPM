using Microsoft.EntityFrameworkCore.Migrations;

namespace doan_cnpm.Data.Migrations
{
    public partial class addtotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalAppointment",
                table: "Appoinments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAppointment",
                table: "Appoinments");
        }
    }
}
