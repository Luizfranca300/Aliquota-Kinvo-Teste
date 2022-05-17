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
    public class ClienteConfiguracao : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {

            builder.ToTable("clientes");
            builder.HasKey(b => b.ClienteId);

            builder
               .Property(b => b.ClienteId)
               .HasColumnName("cliente_id")
               .IsRequired();
            builder
              .Property(b => b.Nome)
              .HasColumnName("nome")
              .IsRequired();
            builder
              .Property(b => b.Cpf)
              .HasColumnName("cpf")
              .IsRequired();

            builder
             .Property(b => b.DataNascimento)
             .HasColumnName("data_nascimento")
             .IsRequired();
            builder
            .Property(b => b.ExcluidoEm)
            .HasColumnName("excluido_em")
            .IsRequired(false);

            builder
                .HasMany(b => b.ProdutoFinanceiro)
                .WithOne(b => b.Cliente)
                .HasForeignKey(b => b.ClienteId);
            builder
                .HasOne(b => b.Carteira)
                .WithOne(b => b.Cliente);
               


        }
    }
}
