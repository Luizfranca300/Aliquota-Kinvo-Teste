using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Entidades
{
    public class ProdutoFinanceiro: Entidade
    {
       

        public int ProdutoFinanceiroId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PorcentagemRentabilidade { get; set; }
        public ICollection<Aplicacao> Aplicacao { get; set; }
        public ICollection<Resgate> Resgate { get; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ProdutoFinanceiro(int produtoFinanceiroId, string nome, string descricao, double porcentagemRentabilidade)
        {
            ProdutoFinanceiroId = produtoFinanceiroId;
            Nome = nome;
            Descricao = descricao;
            PorcentagemRentabilidade = porcentagemRentabilidade;
        }

        public override bool EhValido()
        {
            ValidacaoResultado = new ProdutoFinanceiroValidator().Validate(this);
            return ValidacaoResultado.IsValid;
        }

        public class ProdutoFinanceiroValidator : AbstractValidator<ProdutoFinanceiro>
        {
            public ProdutoFinanceiroValidator()
            {
                ValidarNome();
                ValidarDescricao();
                ValidarPorcentagemRentabilidade();
            }

            private void ValidarPorcentagemRentabilidade()
            {
                RuleFor(x => x.PorcentagemRentabilidade)
                                .NotEmpty()
                                .WithMessage("Porcentagem de Rentabilidade está invalido");
            }

            private void ValidarDescricao()
            {
                RuleFor(x => x.Descricao)
                                .NotEmpty()
                                .WithMessage("Descricao está invalido"); ;
            }

            private void ValidarNome()
            {
                RuleFor(x => x.Nome)
                                 .NotEmpty()
                                 .MaximumLength(60)
                                 .WithMessage("Nome está invalido");
            }
        }
    }
}
