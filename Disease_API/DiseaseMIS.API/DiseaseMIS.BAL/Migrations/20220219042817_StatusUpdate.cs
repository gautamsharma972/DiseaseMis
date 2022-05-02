using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class StatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Incharges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Incharges");
        }
    }
}
