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
    public class CarteiraConfiguracao : IEntityTypeConfiguration<Carteira>
    {
        public void Configure(EntityTypeBuilder<Carteira> builder)
        {

            builder.ToTable("carteira");
            builder.HasKey(b => b.CarteiraId);

            builder
               .Property(b => b.CarteiraId)
               .HasColumnName("carteira_id")
               .IsRequired();
            builder
              .Property(b => b.SaldoConta)
              .HasColumnName("saldo_conta")
              .IsRequired(false);
            builder
              .Property(b => b.SaldoInvestido)
              .HasColumnName("saldo_investido")
              .IsRequired(false);

            builder
             .Property(b => b.ClienteId)
             .HasColumnName("cliente_id")
             .IsRequired();          


        }
    }
}
