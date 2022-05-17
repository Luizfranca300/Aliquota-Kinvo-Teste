using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Entidades
{
    public class Aplicacao : Entidade
    {
        public int AplicacaoId { get; set; }
        public DateTime AplicadoEm { get; set; } = DateTime.Now;
        public double ValorAplicado { get; set; }
        public int ProdutoFinanceiroId { get; set; }
        public ProdutoFinanceiro ProdutoFinanceiro { get; set; }

        public override bool EhValido()
        {
            ValidacaoResultado = new AplicacaoValidator().Validate(this);
           
            return ValidacaoResultado.IsValid;
        }

        public class AplicacaoValidator : AbstractValidator<Aplicacao>
        {
            public AplicacaoValidator()
            {
                ValidarAplicadoEm();
                ValidarValorAplicado();
            }

           

            private void ValidarValorAplicado()
            {
                RuleFor(x => x.ValorAplicado)
                               .NotEmpty()
                               .When(c =>c.ValorAplicado > 0)
                               .WithMessage("Valor Aplicado não pode ser menor ou igual a zero");
            }
            private void ValidarAplicadoEm()
            {
                RuleFor(x => x.AplicadoEm)
                                 .NotEmpty()
                                 .WithMessage("Data da aplicacao está invalido");
            }
        }
    }
}
