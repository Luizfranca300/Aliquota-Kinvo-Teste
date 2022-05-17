using Aliquota.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Infra.Configuracao
{
    public class ProdutoFinanceiroConfiguracao : IEntityTypeConfiguration<ProdutoFinanceiro>
    {
        public void Configure(EntityTypeBuilder<ProdutoFinanceiro> builder)
        {

            builder.ToTable("produtofinaceiros");
            builder.HasKey(b => b.ProdutoFinanceiroId);

            builder
               .Property(b => b.ProdutoFinanceiroId)
               .HasColumnName("produtofinanceiro_id")
               .IsRequired();
            builder
              .Property(b => b.Nome)
              .HasColumnName("nome")
              .IsRequired();
            builder
              .Property(b => b.Descricao)
              .HasColumnName("descricao")
              .IsRequired();

            builder
             .Property(b => b.PorcentagemRentabilidade)
             .HasColumnName("porcentagem_rentabilidade")
             .IsRequired();
            builder
             .Property(b => b.ClienteId)
             .HasColumnName("cliente_id")
             .IsRequired();

            builder
                .HasMany(b => b.Aplicacao)
                .WithOne(b => b.ProdutoFinanceiro)
                .HasForeignKey(b => b.ProdutoFinanceiroId);
            builder
                .HasMany(b => b.Resgate)
                .WithOne(b => b.ProdutoFinanceiro)
                .HasForeignKey(b => b.ProdutoFinanceiroId);


        }
    }
}
