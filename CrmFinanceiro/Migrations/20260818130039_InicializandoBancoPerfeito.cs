using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmFinanceiro.Migrations
{
    /// <inheritdoc />
    public partial class InicializandoBancoPerfeito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoAcoes_TitulosFinanceiros_TituloId1",
                table: "HistoricoAcoes");

            migrationBuilder.DropForeignKey(
                name: "FK_TitulosFinanceiros_Pessoas_PessoaId1",
                table: "TitulosFinanceiros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TitulosFinanceiros",
                table: "TitulosFinanceiros");

            migrationBuilder.DropIndex(
                name: "IX_TitulosFinanceiros_PessoaId1",
                table: "TitulosFinanceiros");

            migrationBuilder.DropColumn(
                name: "PessoaId1",
                table: "TitulosFinanceiros");

            migrationBuilder.RenameTable(
                name: "TitulosFinanceiros",
                newName: "FinanceiroCaixa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinanceiroCaixa",
                table: "FinanceiroCaixa",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceiroCaixa_PessoaId",
                table: "FinanceiroCaixa",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceiroCaixa_Pessoas_PessoaId",
                table: "FinanceiroCaixa",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoAcoes_FinanceiroCaixa_TituloId1",
                table: "HistoricoAcoes",
                column: "TituloId1",
                principalTable: "FinanceiroCaixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinanceiroCaixa_Pessoas_PessoaId",
                table: "FinanceiroCaixa");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoAcoes_FinanceiroCaixa_TituloId1",
                table: "HistoricoAcoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinanceiroCaixa",
                table: "FinanceiroCaixa");

            migrationBuilder.DropIndex(
                name: "IX_FinanceiroCaixa_PessoaId",
                table: "FinanceiroCaixa");

            migrationBuilder.RenameTable(
                name: "FinanceiroCaixa",
                newName: "TitulosFinanceiros");

            migrationBuilder.AddColumn<long>(
                name: "PessoaId1",
                table: "TitulosFinanceiros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitulosFinanceiros",
                table: "TitulosFinanceiros",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TitulosFinanceiros_PessoaId1",
                table: "TitulosFinanceiros",
                column: "PessoaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoAcoes_TitulosFinanceiros_TituloId1",
                table: "HistoricoAcoes",
                column: "TituloId1",
                principalTable: "TitulosFinanceiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitulosFinanceiros_Pessoas_PessoaId1",
                table: "TitulosFinanceiros",
                column: "PessoaId1",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
