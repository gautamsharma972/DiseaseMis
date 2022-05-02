using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class SampleTablAdda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "TestTypes");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_Name",
                table: "Diseases");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
