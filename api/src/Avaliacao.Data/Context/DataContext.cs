using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using Avaliacao.Data.Mappings;
using Avaliacao.Domain.Entities;

namespace Avaliacao.Data.Context
{
    public class DataContext : DbContext
    {

    public DataContext(DbContextOptions<DataContext> options) : base(options) {
        
    }

        public DbSet <Usuario> Usuarios { get; set; }
        public DbSet <Sexo> Sexos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.Entity<Usuario>().HasQueryFilter(p => p.Ativo);
            modelBuilder.ApplyConfiguration(new SexoMap());
            modelBuilder.Entity<Sexo>()
            .HasData(new List<Sexo>(){
                new Sexo(1, "Masculino"),
                new Sexo(2, "Feminino")
            });
          }
    }
}