using Aliquota.Dominio.Entidades;
using Aliquota.Dominio.Repositorios;
using Aliquota.Dominio.Servicoes;
using Aliquota.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aliquota.Test.Services
{
    public class AliquotaServicoTeste
    {


        [Fact]
        public void ClienteEntidadeTeste()
        {
            var ClienteEsperado = new
            {
                ClienteId = 1,
                Nome = "Luiz Fernando Franca",
                Cpf = "116.031.806-99",
                DataNascimento = DateTime.Parse("1999-01-26"),
            };

           var cliente = new Cliente(ClienteEsperado.Nome, ClienteEsperado.Cpf, ClienteEsperado.DataNascimento);
                                 
        }

        [Fact]
        public void ProdutoFinanceiroEntidadeTeste()
        {
            var ProdutoFinanceiroEsperado = new
            {
                ProdutoFinanceiroId = 1,
                Nome = "Cdb",
                Descricao = "Renda fixa ",
                PorcentagemRentabilidade = (double)100,
           
            };

            var produtoFinanceiro = new ProdutoFinanceiro(ProdutoFinanceiroEsperado.ProdutoFinanceiroId, ProdutoFinanceiroEsperado.Nome, ProdutoFinanceiroEsperado.Descricao, ProdutoFinanceiroEsperado.PorcentagemRentabilidade);

        }

    }
}
