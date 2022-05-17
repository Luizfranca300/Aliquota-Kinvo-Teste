using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Entidades
{
    public class Resgate : Entidade
    {
        public int ResgateId { get; set; }
        public DateTime ResgatadoEm { get; set; } = DateTime.Now;
        public double ValorResgatado { get; set; }
        public double? IR { get; set; }
        public double? Lucro { get; set; }
        public int AplicacaoId { get; set; }
        public int ProdutoFinanceiroId { get; set; }
        public ProdutoFinanceiro ProdutoFinanceiro { get; set; }

        public override bool EhValido()
        {
            ValidacaoResultado = new ResgateValidator().Validate(this);

            return ValidacaoResultado.IsValid;
        }
        public class ResgateValidator : AbstractValidator<Resgate>
        {
            public ResgateValidator()
            {
                ValidarResgatadoEm();
                ValidarValorResgatado();
            }

            private void ValidarValorResgatado()
            {
                RuleFor(x => x.ValorResgatado)
                                .NotEmpty()
                                .When(c => c.ValorResgatado > 0)
                                .WithMessage("Valor Aplicado não pode ser menor ou igual a zero");
            }

            private void ValidarResgatadoEm()
            {
                RuleFor(x => x.ResgatadoEm)
                                  .NotEmpty()
                                  .WithMessage("Data da aplicacao está invalido");
            }
        }


    }
}
