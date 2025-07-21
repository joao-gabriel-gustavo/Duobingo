using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duobingo.InfraestruturaEmOrm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBDisciplina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Disciplina_DisciplinaId",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Testes_TesteId",
                table: "Materias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materias",
                table: "Materias");

            migrationBuilder.RenameTable(
                name: "Materias",
                newName: "Materia");

            migrationBuilder.RenameIndex(
                name: "IX_Materias_TesteId",
                table: "Materia",
                newName: "IX_Materia_TesteId");

            migrationBuilder.RenameIndex(
                name: "IX_Materias_DisciplinaId",
                table: "Materia",
                newName: "IX_Materia_DisciplinaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Disciplina",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materia",
                table: "Materia",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Disciplina_DisciplinaId",
                table: "Materia",
                column: "DisciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Testes_TesteId",
                table: "Materia",
                column: "TesteId",
                principalTable: "Testes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Disciplina_DisciplinaId",
                table: "Materia");

            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Testes_TesteId",
                table: "Materia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materia",
                table: "Materia");

            migrationBuilder.RenameTable(
                name: "Materia",
                newName: "Materias");

            migrationBuilder.RenameIndex(
                name: "IX_Materia_TesteId",
                table: "Materias",
                newName: "IX_Materias_TesteId");

            migrationBuilder.RenameIndex(
                name: "IX_Materia_DisciplinaId",
                table: "Materias",
                newName: "IX_Materias_DisciplinaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Disciplina",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materias",
                table: "Materias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Disciplina_DisciplinaId",
                table: "Materias",
                column: "DisciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Testes_TesteId",
                table: "Materias",
                column: "TesteId",
                principalTable: "Testes",
                principalColumn: "Id");
        }
    }
}
