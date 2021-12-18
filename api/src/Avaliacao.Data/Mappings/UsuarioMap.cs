using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Avaliacao.Domain.Entities;


namespace Avaliacao.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder) {

            builder.ToTable("Usuarios");

            builder.HasKey( a => a.UsuarioId);

            builder.Property( a => a.Nome)
                .HasColumnType("varchar(200)");

             builder.Property( a => a.Email)
                .HasColumnType("varchar(100)");

             builder.Property( a => a.Senha)
                .HasColumnType("varchar(30)");             

            builder.HasOne(a => a.Sexo)
                .WithMany( p => p.Usuarios)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
                

        }
        
    }
}