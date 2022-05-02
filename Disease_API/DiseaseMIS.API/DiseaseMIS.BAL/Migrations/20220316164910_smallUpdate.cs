using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class smallUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingOrder",
                table: "Samples",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingOrder",
                table: "Samples");
        }
    }
}
