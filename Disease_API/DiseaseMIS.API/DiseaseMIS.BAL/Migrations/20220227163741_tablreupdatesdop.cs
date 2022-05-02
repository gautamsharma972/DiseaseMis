using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tablreupdatesdop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormName",
                table: "LaboratoryForms");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "LaboratoryForms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "LaboratoryForms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "LaboratoryForms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "LaboratoryForms");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "LaboratoryForms");

            migrationBuilder.DropColumn(
                name: "User",
                table: "LaboratoryForms");

            migrationBuilder.AddColumn<string>(
                name: "FormName",
                table: "LaboratoryForms",
                type: "text",
                nullable: true);
        }
    }
}
