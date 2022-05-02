using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tablreupdatesdo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaboratoryForms",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DistrictId = table.Column<byte[]>(nullable: true),
                    IsLocked = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    FormName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryForms_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabFormValues",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    SamplesId = table.Column<byte[]>(nullable: true),
                    TestTypesId = table.Column<byte[]>(nullable: true),
                    AnimalCategoryId = table.Column<byte[]>(nullable: true),
                    TotalValue = table.Column<int>(nullable: false),
                    NoOfPositiveCases = table.Column<int>(nullable: false),
                    LaboratoryFormsId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabFormValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabFormValues_AnimalCategories_AnimalCategoryId",
                        column: x => x.AnimalCategoryId,
                        principalTable: "AnimalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabFormValues_LaboratoryForms_LaboratoryFormsId",
                        column: x => x.LaboratoryFormsId,
                        principalTable: "LaboratoryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabFormValues_Samples_SamplesId",
                        column: x => x.SamplesId,
                        principalTable: "Samples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabFormValues_TestTypes_TestTypesId",
                        column: x => x.TestTypesId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabFormValues_AnimalCategoryId",
                table: "LabFormValues",
                column: "AnimalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LabFormValues_LaboratoryFormsId",
                table: "LabFormValues",
                column: "LaboratoryFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_LabFormValues_SamplesId",
                table: "LabFormValues",
                column: "SamplesId");

            migrationBuilder.CreateIndex(
                name: "IX_LabFormValues_TestTypesId",
                table: "LabFormValues",
                column: "TestTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryForms_DistrictId",
                table: "LaboratoryForms",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabFormValues");

            migrationBuilder.DropTable(
                name: "LaboratoryForms");
        }
    }
}
