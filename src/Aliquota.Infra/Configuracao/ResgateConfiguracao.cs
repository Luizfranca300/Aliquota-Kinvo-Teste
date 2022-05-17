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
    public class ResgateConfiguracao : IEntityTypeConfiguration<Resgate>
    {

        public void Configure(EntityTypeBuilder<Resgate> builder)
        {
            builder.ToTable("resgates");
            builder.HasKey(b => b.ResgateId);

            builder
                .Property(b => b.ResgateId)
                .HasColumnName("resgate_id")
                .IsRequired();
            builder
                .Property(b => b.ResgatadoEm)
                .HasColumnName("resgatado_em")
                .IsRequired();
            builder
               .Property(b => b.ValorResgatado)
               .HasColumnName("valor_resgatado")
               .IsRequired();
            builder
               .Property(b => b.IR)
               .HasColumnName("ir")
               .IsRequired();
            builder
               .Property(b => b.Lucro)
               .HasColumnName("lucro")
               .IsRequired();

            builder
               .Property(b => b.ProdutoFinanceiroId)
               .HasColumnName("produto_financeiro_id")
               .IsRequired();

        }
    }
}
