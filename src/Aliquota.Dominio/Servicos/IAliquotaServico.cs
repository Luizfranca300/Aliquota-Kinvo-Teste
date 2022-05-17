using Aliquota.Dominio.Entidades;
using Aliquota.Dominio.Repositorios;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Servicoes
{
    public interface IAliquotaServico
    {
        Task<Cliente> SaveAsync();
        ValidationResult InserirCliente(Cliente cliente);
        Task<ValidationResult> AtualizarClienteAsync(Cliente cliente);
        Task<Cliente> DeleteClienteAsync(int clienteId);
        Task<List<Cliente>> ObterTodosClientesAsync(int skip = 0, int take = 1000);
        Task<Cliente> ObterClientePorIdAsync(int clienteId);
        Task<List<Cliente>> ObterClientesSomenteExcluidosAsync();
        Task<Cliente> RestaurarClienteAsync(int clienteId);

        ValidationResult InserirProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro, int clienteId);
        Task<ProdutoFinanceiro> ObterProdutoFinanceiroPorIdAsync(int produtoFinanceiroId);

        ValidationResult InserirAplicacao(Aplicacao aplicacao, int clienteId, int produtoFinanceiroId);
        ValidationResult InserirResgate(Resgate resgate, int produtoFinanceiroId, int clienteId);

        ValidationResult AdicionarCarteira(Carteira carteira, int clienteId);
        Task<ValidationResult> AtualizarSaldoCarteiraAsync(Carteira carteira, int clienteId);
    }
}
