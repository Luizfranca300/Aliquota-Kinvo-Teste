using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Entidades
{
    public class Carteira:Entidade
    {
        public int CarteiraId { get; set; }
        public double? SaldoConta { get; set; }
        public double? SaldoInvestido { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public override bool EhValido()
        {
            ValidacaoResultado = new CarteiraValidator().Validate(this);
            return ValidacaoResultado.IsValid;
        }
        public class CarteiraValidator : AbstractValidator<Carteira>
        {
            public CarteiraValidator()
            {

            }
        }
    }

    
}
