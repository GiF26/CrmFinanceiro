using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmFinanceiro.Migrations
{
    /// <inheritdoc />
    public partial class InicializandoBanco : Migration
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
                name: "TitulosFinanceiros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroDocumento = table.Column<string>(type: "TEXT", nullable: false),
                    TipoDocumento = table.Column<string>(type: "TEXT", nullable: false),
                    PessoaId = table.Column<int>(type: "INTEGER", nullable: false),
                    PessoaId1 = table.Column<long>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StatusTitulo = table.Column<int>(type: "INTEGER", nullable: false),
                    IsConciliado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitulosFinanceiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitulosFinanceiros_Pessoas_PessoaId1",
                        column: x => x.PessoaId1,
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
                    TituloId = table.Column<int>(type: "INTEGER", nullable: false),
                    TituloId1 = table.Column<long>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId1 = table.Column<long>(type: "INTEGER", nullable: false),
                    DataAcao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TipoAcao = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoAcoes_TitulosFinanceiros_TituloId1",
                        column: x => x.TituloId1,
                        principalTable: "TitulosFinanceiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoAcoes_Usuarios_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAcoes_TituloId1",
                table: "HistoricoAcoes",
                column: "TituloId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAcoes_UsuarioId1",
                table: "HistoricoAcoes",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_TitulosFinanceiros_PessoaId1",
                table: "TitulosFinanceiros",
                column: "PessoaId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoAcoes");

            migrationBuilder.DropTable(
                name: "TitulosFinanceiros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
