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
    public class AplicacaoConfiguracao : IEntityTypeConfiguration<Aplicacao>
    {
        public void Configure(EntityTypeBuilder<Aplicacao> builder)
        {
            builder.ToTable("aplicacoes");
            builder.HasKey(b => b.AplicacaoId);

            builder
                .Property(b => b.AplicacaoId)
                .HasColumnName("aplicacao_id")
                .IsRequired();
            builder
                .Property(b => b.AplicadoEm)
                .HasColumnName("aplicado_em")
                .IsRequired();
            builder
               .Property(b => b.ValorAplicado)
               .HasColumnName("valor_aplicado")
               .IsRequired();
            builder
               .Property(b => b.ProdutoFinanceiroId)
               .HasColumnName("produto_financeiro_id")
               .IsRequired();

        }
    }
}
