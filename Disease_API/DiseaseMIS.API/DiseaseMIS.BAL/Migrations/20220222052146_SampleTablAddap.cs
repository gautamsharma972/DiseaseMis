using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class SampleTablAddap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Samples",
               columns: table => new
               {
                   Id = table.Column<byte[]>(nullable: false),
                   Name = table.Column<string>(nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Samples", x => x.Id);
               });

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SampleId = table.Column<byte[]>(nullable: true),
                    TestTypesId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTypes_Samples_SamplesId",
                        column: x => x.SampleId,
                        principalTable: "Samples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTypes_TestTypes_TestTypesId",
                        column: x => x.TestTypesId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_Name",
                table: "Diseases",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Samples_Name",
                table: "Samples",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_SamplesId",
                table: "TestTypes",
                column: "SampleId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_TestTypesId",
                table: "TestTypes",
                column: "TestTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
