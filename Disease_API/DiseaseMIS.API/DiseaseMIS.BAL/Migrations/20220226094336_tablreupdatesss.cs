using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tablreupdatesss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormType",
                table: "DiseaseForms");

            migrationBuilder.AddColumn<int>(
                name: "ValueOfType",
                table: "FormDiseaseValues",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueOfType",
                table: "FormDiseaseValues");

            migrationBuilder.AddColumn<int>(
                name: "FormType",
                table: "DiseaseForms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
