using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Maps
{
    public class AplicacaoMapping : IEntityTypeConfiguration<Aplicacao>
    {
        public void Configure(EntityTypeBuilder<Aplicacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("ID_APLICACAO");

            builder.Property(e => e.Nome)
                    .HasColumnName("NOME")
                   .HasColumnType("varchar(30)")
                   .IsRequired();
           
            
        }
    }
}
