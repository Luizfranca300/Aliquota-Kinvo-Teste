using Aliquota.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Repositorios
{
    public interface IAliquotaRepositorio
    {
        Task<Cliente> SaveAsync();
        void AdicionarCliente(Cliente cliente);
        Task<Cliente> AtualizarClienteAsync(Cliente cliente);
        Task<Cliente> DeleteClienteAsync(int clienteId);
        Task<List<Cliente>> ObterTodosClientesAsync(int skip = 0, int take = 1000);
        Task<Cliente> ObterClientePorIdAsync(int clienteId);
        Task<List<Cliente>> ObterClientesSomenteExcluidosAsync();
        Task<Cliente> RestaurarClienteAsync(int clienteId);

        void AdicionarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro, int clienteId);
        Task<ProdutoFinanceiro> ObterProdutoFinanceiroPorIdAsync(int produtoFinanceiroId);
        void AdicionarAplicacao(Aplicacao aplicacao, int clienteId, int produtoFinanceiroId);

        void AdicionarResgate(Resgate resgate, int produtoFinanceiroId, int clienteId);

        void AdicionarCarteira(Carteira carteira, int clienteId);

        Task<Carteira> AtualizarSaldoCarteiraAsync(Carteira carteira, int clienteId);



    }
}
