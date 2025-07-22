using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duobingo.InfraestruturaEmOrm.Migrations
{
    /// <inheritdoc />
    public partial class Add_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes");

            migrationBuilder.AddForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testes_Materias_MateriaId",
                table: "Testes");

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
