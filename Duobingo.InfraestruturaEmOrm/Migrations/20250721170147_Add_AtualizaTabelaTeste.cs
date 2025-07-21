using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duobingo.InfraestruturaEmOrm.Migrations
{
    /// <inheritdoc />
    public partial class Add_AtualizaTabelaTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questoes_Testes_TesteId",
                table: "Questoes");

            migrationBuilder.DropIndex(
                name: "IX_Questoes_TesteId",
                table: "Questoes");

            migrationBuilder.DropColumn(
                name: "TesteId",
                table: "Questoes");
        }
    }
}
