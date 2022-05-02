using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tableUpdate_statusk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SymptomId",
                table: "Symptoms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SymptomId",
                table: "Symptoms",
                type: "varbinary(16)",
                nullable: true);
        }
    }
}
