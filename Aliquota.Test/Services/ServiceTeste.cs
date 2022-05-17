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
        private readonly AliquotaServico _aliquotaServico;
        Cliente cliente = new Cliente();
       
        public AliquotaServicoTeste()
        {
            _aliquotaServico = new AliquotaServico(new Mock<IAliquotaRepositorio>().Object);
        }

       [Fact]
       public void InserirClienteTest()
        {
            cliente.Nome = "Luiz Fernando Franca";
            cliente.Cpf = "116.031.806-99";
            cliente.DataNascimento = DateTime.Parse("1999-01-26");
            _aliquotaServico.InserirCliente(cliente);
                          
        }

    }
}
