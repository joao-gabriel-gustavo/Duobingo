using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duobingo.InfraestruturaEmOrm.Migrations
{
    /// <inheritdoc />
    public partial class Add_ElementosTabelaTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questoes_Testes_TesteId",
                table: "Questoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes");

            migrationBuilder.DropIndex(
                name: "IX_Questoes_TesteId",
                table: "Questoes");

            migrationBuilder.DropColumn(
                name: "TesteId",
                table: "Questoes");

            migrationBuilder.CreateTable(
                name: "QuestoesTeste",
                columns: table => new
                {
                    QuestoesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestoesTeste", x => new { x.QuestoesId, x.TestesId });
                    table.ForeignKey(
                        name: "FK_QuestoesTeste_Questoes_QuestoesId",
                        column: x => x.QuestoesId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestoesTeste_Testes_TestesId",
                        column: x => x.TestesId,
                        principalTable: "Testes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestoesTeste_TestesId",
                table: "QuestoesTeste",
                column: "TestesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes");

            migrationBuilder.DropTable(
                name: "QuestoesTeste");

            migrationBuilder.AddColumn<Guid>(
                name: "TesteId",
                table: "Questoes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questoes_TesteId",
                table: "Questoes",
                column: "TesteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questoes_Testes_TesteId",
                table: "Questoes",
                column: "TesteId",
                principalTable: "Testes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
