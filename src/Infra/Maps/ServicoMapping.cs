using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Maps
{
    public class ServicoMapping : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .HasColumnName("ID_SERVICO");
            
            builder.Property(e => e.Id)
                    .IsRequired();
            
            builder.Property(e => e.IdAplicacao)
                    .HasColumnName("ID_APLICACAO")
                    .IsRequired();
            
            builder.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasColumnType("varchar(40)")
                    .IsRequired();
            
            //Aqui Foi Criado um relacionamento entre a Aplicação e os serviços
            builder.HasOne(e => e.Aplicacao)
            .WithMany(e => e.Servicos)
            .HasForeignKey(e => e.IdAplicacao)
            .IsRequired();
        }
    }
}
