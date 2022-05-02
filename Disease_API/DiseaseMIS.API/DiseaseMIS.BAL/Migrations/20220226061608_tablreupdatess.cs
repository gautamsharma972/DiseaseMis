using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tablreupdatess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentStep",
                table: "DiseaseForms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "DiseaseForms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStep",
                table: "DiseaseForms");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "DiseaseForms");
        }
    }
}
