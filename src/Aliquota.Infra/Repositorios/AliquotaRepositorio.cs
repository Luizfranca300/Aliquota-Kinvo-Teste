using Aliquota.Dominio.Entidades;
using Aliquota.Dominio.Repositorios;
using Aliquota.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Infra.Repositorios
{
    public class AliquotaRepositorio : IAliquotaRepositorio
    {

        private readonly DbSet<Cliente> _clientes;
        private readonly DbSet<ProdutoFinanceiro> _produtoFinanceiros;
        private readonly DbSet<Aplicacao> _aplicacao;
        private readonly DbSet<Resgate> _resgate;
        private readonly DbSet<Carteira> _carteira;
        private readonly DataContext _context;

        public AliquotaRepositorio(DataContext dataContext)
        {
            _clientes = dataContext.Set<Cliente>();
            _produtoFinanceiros = dataContext.Set<ProdutoFinanceiro>();
            _aplicacao = dataContext.Set<Aplicacao>();
            _resgate = dataContext.Set<Resgate>();
            _carteira = dataContext.Set<Carteira>();
            _context = dataContext;
        }
        public async Task<Cliente> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return null;
        }

        public void AdicionarCliente(Cliente cliente)
        {
            _clientes.Add(cliente);
        }
        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            var itemdoBanco = await _clientes
                .FirstOrDefaultAsync(c => c.ClienteId == cliente.ClienteId);

            if (itemdoBanco != null)
            {
                _context.Entry(itemdoBanco).CurrentValues.SetValues(cliente);

                return cliente;
            }
            return null;
        }
        public async Task<Cliente> DeleteClienteAsync(int clienteId)
        {
            var result = await _clientes
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);

            if (result != null)
            {
                result.ExcluidoEm = DateTime.Now;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<List<Cliente>> ObterTodosClientesAsync(int skip = 0, int take = 1000)
        {
            var result = await _clientes
                          .AsNoTracking()
                          .Skip(skip)
                          .Take(take)
                          .ToListAsync();
            return result;
        }
        public async Task<Cliente> ObterClientePorIdAsync(int clienteId)
        {
            var result = await _clientes
                .Include(c=>c.Carteira)
                .Include(c => c.ProdutoFinanceiro).ThenInclude(c => c.Aplicacao)
                .Include(c =>c.ProdutoFinanceiro).ThenInclude(c =>c.Resgate)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
            return result;
        }
        public async Task<List<Cliente>> ObterClientesSomenteExcluidosAsync()
        {
            var result = await _clientes
                .IgnoreQueryFilters()
                .Where(c => c.ExcluidoEm.HasValue)
                .ToListAsync();

            return result;
        }
        public async Task<Cliente> RestaurarClienteAsync(int clienteId)
        {
            var result = await _clientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);

            if (result != null)
            {
                result.ExcluidoEm = null;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }

        public void AdicionarCarteira(Carteira carteira, int clienteId)
        {
            carteira.ClienteId = clienteId;
            carteira.SaldoConta = 0;
            carteira.SaldoInvestido = 0;
            _carteira.Add(carteira);
        }
        public async Task<Carteira> AtualizarSaldoCarteiraAsync(Carteira carteira, int clienteId)
        {
            var itemdoBanco = await _carteira
                .FirstOrDefaultAsync(c => c.ClienteId == carteira.ClienteId);

            if (itemdoBanco != null)
            {
                carteira.ClienteId = clienteId;
                _context.Entry(itemdoBanco).CurrentValues.SetValues(carteira);

                return carteira;
            }
            return null;
        }

        public void AdicionarProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro, int clienteId)
        {
            produtoFinanceiro.ClienteId = clienteId;
            _produtoFinanceiros.Add(produtoFinanceiro);
        }
        public async Task<ProdutoFinanceiro> ObterProdutoFinanceiroPorIdAsync(int produtoFinanceiroId)
        {
            var result = await _produtoFinanceiros
                .FirstOrDefaultAsync(c => c.ProdutoFinanceiroId == produtoFinanceiroId);
            return result;
        }

        public void AdicionarAplicacao(Aplicacao aplicacao,int clienteId, int produtoFinanceiroId)
        {
            var resultcarteira = _carteira.FirstOrDefault(c =>c.ClienteId == clienteId);
            aplicacao.ProdutoFinanceiroId = produtoFinanceiroId;
            var result = _produtoFinanceiros
                .FirstOrDefault(c => c.ClienteId == clienteId);
            if (result != null )
            {
                resultcarteira.SaldoConta -= aplicacao.ValorAplicado;
                resultcarteira.SaldoInvestido += aplicacao.ValorAplicado;
                _context.Entry(resultcarteira).State = EntityState.Modified;
                _aplicacao.Add(aplicacao);
            }
       
        }

        public void AdicionarResgate(Resgate resgate, int produtoFinanceiroId, int clienteId)
        {
            resgate.ProdutoFinanceiroId = produtoFinanceiroId;
            var resultcarteira = _carteira.FirstOrDefault(c => c.ClienteId == clienteId);
            var resultProdutoFinanceiro = _produtoFinanceiros
                .Where(c => c.ClienteId == clienteId)
                .FirstOrDefault(C=>C.ProdutoFinanceiroId == produtoFinanceiroId);
            var resultAplicacao = _aplicacao
                .Where(c => c.AplicacaoId == resgate.AplicacaoId)
                .FirstOrDefault(c => c.ProdutoFinanceiroId == produtoFinanceiroId);
            TimeSpan timeSpan = resgate.ResgatadoEm.Subtract(resultAplicacao.AplicadoEm);
             double lucroBruto = (resgate.ValorResgatado*timeSpan.Days*(resultProdutoFinanceiro.PorcentagemRentabilidade/360));
            //double lucroBruto = resgate.ValorResgatado *(1+Math.Pow((resultProdutoFinanceiro.PorcentagemRentabilidade / 360),timeSpan.Days)); -> juros composto
            if (timeSpan.Days <= 360)
                resgate.IR = lucroBruto * 0.225;
            else if (timeSpan.Days >= 360 && timeSpan.Days <= 720)
                resgate.IR = lucroBruto * 0.185;
            else if (timeSpan.Days >= 720)
                resgate.IR = lucroBruto * 0.15;
            resgate.Lucro = lucroBruto - resgate.IR;
            resultcarteira.SaldoInvestido -= resgate.ValorResgatado;
            resultcarteira.SaldoConta += resgate.ValorResgatado;
            _context.Entry(resultcarteira).State = EntityState.Modified;
            _resgate.Add(resgate);
        }
    }
    }
