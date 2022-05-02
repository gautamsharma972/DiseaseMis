using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiseaseMIS.BAL.Migrations
{
    public partial class tablreupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDiseases");

            migrationBuilder.CreateTable(
                name: "FormDiseaseValues",
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
                    table.PrimaryKey("PK_FormDiseaseValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormDiseaseValues_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormDiseaseValues_DiseaseForms_DiseaseFormsId",
                        column: x => x.DiseaseFormsId,
                        principalTable: "DiseaseForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormDiseaseValues_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormDiseaseValues_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseaseValues_AnimalId",
                table: "FormDiseaseValues",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseaseValues_DiseaseFormsId",
                table: "FormDiseaseValues",
                column: "DiseaseFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseaseValues_DiseaseId",
                table: "FormDiseaseValues",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_FormDiseaseValues_SymptomId",
                table: "FormDiseaseValues",
                column: "SymptomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDiseaseValues");

            migrationBuilder.CreateTable(
                name: "FormDiseases",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    AnimalId = table.Column<byte[]>(type: "varbinary(16)", nullable: true),
                    DiseaseFormsId = table.Column<byte[]>(type: "varbinary(16)", nullable: true),
                    DiseaseId = table.Column<byte[]>(type: "varbinary(16)", nullable: true),
                    SymptomId = table.Column<byte[]>(type: "varbinary(16)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false)
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
    }
}
