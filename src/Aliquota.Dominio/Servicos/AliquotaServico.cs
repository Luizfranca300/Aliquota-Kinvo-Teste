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
    public class AliquotaServico : IAliquotaServico
    {

        private readonly IAliquotaRepositorio _aliquotaRepositorio;

        public AliquotaServico(IAliquotaRepositorio aliquotaRepositorio)
        {
            _aliquotaRepositorio = aliquotaRepositorio;
        }

        public async Task<Cliente> SaveAsync()
        {
            return await _aliquotaRepositorio.SaveAsync();
        }

        public ValidationResult InserirCliente(Cliente cliente)
        {
            if (!cliente.EhValido())
            {
                return cliente.ValidacaoResultado;
            }

            _aliquotaRepositorio.AdicionarCliente(cliente);
            return new ValidationResult();
        }
        public async Task<ValidationResult> AtualizarClienteAsync(Cliente cliente)
        {
            if (!cliente.EhValido())
            {
                return cliente.ValidacaoResultado;
            }

            await _aliquotaRepositorio.AtualizarClienteAsync(cliente);
            return new ValidationResult();
        }
        public async Task<Cliente> DeleteClienteAsync(int clienteId)
        {
           return await _aliquotaRepositorio.DeleteClienteAsync(clienteId);
        }
        public async Task<List<Cliente>> ObterTodosClientesAsync(int skip = 0, int take = 1000)
        {
            return await _aliquotaRepositorio.ObterTodosClientesAsync(skip, take);
        }
        public Task<Cliente> ObterClientePorIdAsync(int clienteId)
        {
            return _aliquotaRepositorio.ObterClientePorIdAsync(clienteId);
        }
        public async Task<List<Cliente>> ObterClientesSomenteExcluidosAsync()
        {
            return await _aliquotaRepositorio.ObterClientesSomenteExcluidosAsync();
        }
        public async Task<Cliente> RestaurarClienteAsync(int clienteId)
        {
            return await _aliquotaRepositorio.RestaurarClienteAsync(clienteId);
        }

        public ValidationResult InserirProdutoFinanceiro(ProdutoFinanceiro produtoFinanceiro, int clienteId)
        {
            if (!produtoFinanceiro.EhValido())
            {
                return produtoFinanceiro.ValidacaoResultado;
            }

            _aliquotaRepositorio.AdicionarProdutoFinanceiro(produtoFinanceiro, clienteId);
            return new ValidationResult();
        }
        public Task<ProdutoFinanceiro> ObterProdutoFinanceiroPorIdAsync(int produtoFinanceiroId)
        {
            return _aliquotaRepositorio.ObterProdutoFinanceiroPorIdAsync(produtoFinanceiroId);
        }

        public ValidationResult InserirAplicacao(Aplicacao aplicacao, int clienteId, int produtoFinanceiroId)
        {
            if (!aplicacao.EhValido())
            {
                return aplicacao.ValidacaoResultado;
            }


            _aliquotaRepositorio.AdicionarAplicacao(aplicacao,clienteId,produtoFinanceiroId);
            return new ValidationResult();
        }

        public ValidationResult InserirResgate(Resgate resgate, int produtoFinanceiroId, int clienteId)
        {
            if (!resgate.EhValido())
            {
                return resgate.ValidacaoResultado;
            }


            _aliquotaRepositorio.AdicionarResgate(resgate, produtoFinanceiroId, clienteId);
            return new ValidationResult();
        }

        public ValidationResult AdicionarCarteira(Carteira carteira, int clienteId)
        {
            if (!carteira.EhValido())
            {
                return carteira.ValidacaoResultado;
            }


            _aliquotaRepositorio.AdicionarCarteira(carteira, clienteId);
            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarSaldoCarteiraAsync(Carteira carteira, int clienteId)

        {
            if (!carteira.EhValido())
            {
                return carteira.ValidacaoResultado;
            }

             await _aliquotaRepositorio.AtualizarSaldoCarteiraAsync(carteira, clienteId);
            return new ValidationResult();
        }
    }
}
