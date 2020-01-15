using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Maps
{
    public class LogMapping : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Hora)
                    .HasColumnName("HORA")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(e => e.Tipo)
                    .HasColumnName("Tipo")
                    .HasColumnType("varchar(15)")
                    .IsRequired();

            builder.Property(e => e.Log)
                    .HasColumnName("Log")
                    .HasColumnType("varchar(150)")
                    .IsRequired();

            builder.HasOne(e => e.Execucao)
                    .WithMany(e => e.Logs)
                    .HasForeignKey(e => e.Execucao.Id)
                    .IsRequired();
        }
    }
}
