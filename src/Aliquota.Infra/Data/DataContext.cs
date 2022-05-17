using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliquota.Infra.Configuracao;
using Aliquota.Dominio.Entidades;

namespace Aliquota.Infra.Data
{
   public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> contextOptions) : base(contextOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfiguration(new ClienteConfiguracao());
            modelBuilder.ApplyConfiguration(new ProdutoFinanceiroConfiguracao());
            modelBuilder.ApplyConfiguration(new AplicacaoConfiguracao());
            modelBuilder.ApplyConfiguration(new ResgateConfiguracao());
            modelBuilder.ApplyConfiguration(new CarteiraConfiguracao());
            modelBuilder.Entity<Cliente>().HasQueryFilter(p => !p.ExcluidoEm.HasValue);

        }


    }
}
