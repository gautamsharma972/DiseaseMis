using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class FormsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiseaseForms",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    FormType = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DistrictId = table.Column<byte[]>(nullable: true),
                    InstituteId = table.Column<byte[]>(nullable: true),
                    InchargeId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiseaseForms_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiseaseForms_Incharges_InchargeId",
                        column: x => x.InchargeId,
                        principalTable: "Incharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiseaseForms_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormDiseases",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    DiseaseId = table.Column<byte[]>(nullable: true),
                    SymptomId = table.Column<byte[]>(nullable: true),
                    AnimalId = table.Column<byte[]>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    DiseaseFormsId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormDiseases_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormDiseases_DiseaseForms_DiseaseFormsId",
                        column: x => x.DiseaseFormsId,
                        principalTable: "DiseaseForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormDiseases_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseForms_DistrictId",
                table: "DiseaseForms",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseForms_InchargeId",
                table: "DiseaseForms",
                column: "InchargeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseForms_InstituteId",
                table: "DiseaseForms",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseases_AnimalId",
                table: "FormDiseases",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseases_DiseaseFormsId",
                table: "FormDiseases",
                column: "DiseaseFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseases_DiseaseId",
                table: "FormDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseases_SymptomId",
                table: "FormDiseases",
                column: "SymptomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDiseases");

            migrationBuilder.DropTable(
                name: "DiseaseForms");
        }
    }
}
