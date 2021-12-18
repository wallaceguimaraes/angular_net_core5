using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Avaliacao.Domain.Entities;

namespace Avaliacao.Data.Mappings
{
    public class SexoMap : IEntityTypeConfiguration <Sexo>
    {
          public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder <Sexo> builder) {

            builder.ToTable("Sexos");

            builder.HasKey( a => a.SexoId);

            builder.Property( a => a.Descricao)
                .HasColumnType("varchar(15)");    

                
             
        }
    }
}