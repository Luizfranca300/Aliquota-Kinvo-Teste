using FluentValidation.Results;

namespace Aliquota.Dominio.Entidades
{
    public abstract class Entidade
    {
        public ValidationResult ValidacaoResultado { get; set; }

        public abstract bool EhValido();


    }
}
