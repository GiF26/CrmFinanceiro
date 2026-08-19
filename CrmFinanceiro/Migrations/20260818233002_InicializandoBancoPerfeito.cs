using System;
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
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    CpfCnpj = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    TipoPessoa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinanceiroCaixa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroDocumento = table.Column<string>(type: "TEXT", nullable: false),
                    TipoDocumento = table.Column<string>(type: "TEXT", nullable: false),
                    PessoaId = table.Column<long>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StatusTitulo = table.Column<int>(type: "INTEGER", nullable: false),
                    IsConciliado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceiroCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceiroCaixa_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoAcoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TituloId = table.Column<long>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<long>(type: "INTEGER", nullable: false),
                    DataAcao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TipoAcao = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoAcoes_FinanceiroCaixa_TituloId",
                        column: x => x.TituloId,
                        principalTable: "FinanceiroCaixa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoAcoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceiroCaixa_PessoaId",
                table: "FinanceiroCaixa",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAcoes_TituloId",
                table: "HistoricoAcoes",
                column: "TituloId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAcoes_UsuarioId",
                table: "HistoricoAcoes",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoAcoes");

            migrationBuilder.DropTable(
                name: "FinanceiroCaixa");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
