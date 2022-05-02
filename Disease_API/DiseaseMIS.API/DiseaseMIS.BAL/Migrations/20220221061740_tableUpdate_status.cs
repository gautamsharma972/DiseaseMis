using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tableUpdate_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SymptomId",
                table: "Symptoms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
