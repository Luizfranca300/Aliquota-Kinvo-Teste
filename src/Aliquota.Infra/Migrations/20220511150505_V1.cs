using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aliquota.Infra.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    cliente_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    excluido_em = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_nascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.cliente_id);
                });

            migrationBuilder.CreateTable(
                name: "carteira",
                columns: table => new
                {
                    carteira_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saldo_conta = table.Column<double>(type: "float", nullable: false),
                    saldo_investido = table.Column<double>(type: "float", nullable: true),
                    cliente_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carteira", x => x.carteira_id);
                    table.ForeignKey(
                        name: "FK_carteira_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produtofinaceiros",
                columns: table => new
                {
                    produtofinanceiro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    porcentagem_rentabilidade = table.Column<double>(type: "float", nullable: false),
                    cliente_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtofinaceiros", x => x.produtofinanceiro_id);
                    table.ForeignKey(
                        name: "FK_produtofinaceiros_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aplicacoes",
                columns: table => new
                {
                    aplicacao_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aplicado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor_aplicado = table.Column<double>(type: "float", nullable: false),
                    produto_financeiro_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aplicacoes", x => x.aplicacao_id);
                    table.ForeignKey(
                        name: "FK_aplicacoes_produtofinaceiros_produto_financeiro_id",
                        column: x => x.produto_financeiro_id,
                        principalTable: "produtofinaceiros",
                        principalColumn: "produtofinanceiro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "resgates",
                columns: table => new
                {
                    resgate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resgatado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor_resgatado = table.Column<double>(type: "float", nullable: false),
                    ir = table.Column<double>(type: "float", nullable: false),
                    lucro = table.Column<double>(type: "float", nullable: false),
                    AplicacaoId = table.Column<int>(type: "int", nullable: false),
                    produto_financeiro_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resgates", x => x.resgate_id);
                    table.ForeignKey(
                        name: "FK_resgates_produtofinaceiros_produto_financeiro_id",
                        column: x => x.produto_financeiro_id,
                        principalTable: "produtofinaceiros",
                        principalColumn: "produtofinanceiro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aplicacoes_produto_financeiro_id",
                table: "aplicacoes",
                column: "produto_financeiro_id");

            migrationBuilder.CreateIndex(
                name: "IX_carteira_cliente_id",
                table: "carteira",
                column: "cliente_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produtofinaceiros_cliente_id",
                table: "produtofinaceiros",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_resgates_produto_financeiro_id",
                table: "resgates",
                column: "produto_financeiro_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aplicacoes");

            migrationBuilder.DropTable(
                name: "carteira");

            migrationBuilder.DropTable(
                name: "resgates");

            migrationBuilder.DropTable(
                name: "produtofinaceiros");

            migrationBuilder.DropTable(
                name: "clientes");
        }
    }
}
