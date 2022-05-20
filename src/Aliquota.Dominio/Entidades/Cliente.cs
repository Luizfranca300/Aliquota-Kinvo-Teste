using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Dominio.Entidades
{
    public class Cliente : Entidade
    {
       

        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<ProdutoFinanceiro> ProdutoFinanceiro{ get;}
        public Carteira Carteira { get; set; }

        public Cliente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public override bool EhValido()
        {
            ValidacaoResultado = new ClienteValidator().Validate(this);
           
            return ValidacaoResultado.IsValid;
        }

        public class ClienteValidator : AbstractValidator<Cliente> {
        public ClienteValidator()
            {
                ValidarNome();
                ValidarCpf();
                ValidarDataNascimento();
            }

            private void ValidarDataNascimento()
            {
                RuleFor(x => x.DataNascimento)
                                .NotEmpty()
                                .WithMessage("Data de Nascimento está invalido");
            }

            private void ValidarCpf()
            {
                RuleFor(x => x.Cpf)
                                .NotEmpty()
                                .MaximumLength(14)
                                .WithMessage("Cpf está invalido"); ;
            }

            private void ValidarNome()
            {
                RuleFor(x => x.Nome)
                                 .NotEmpty()
                                 .MaximumLength(100)
                                 .WithMessage("Nome está invalido");
            }
        }

    }
}
