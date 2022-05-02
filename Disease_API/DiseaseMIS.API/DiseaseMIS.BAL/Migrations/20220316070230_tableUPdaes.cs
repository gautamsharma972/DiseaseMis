using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tableUPdaes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingOrder",
                table: "Diseases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ListingOrder",
                table: "Animals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingOrder",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "ListingOrder",
                table: "Animals");
        }
    }
}
