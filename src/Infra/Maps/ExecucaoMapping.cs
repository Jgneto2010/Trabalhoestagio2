using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Maps
{
    public class ExecucaoMapping : IEntityTypeConfiguration<Execucao>
    {
        public void Configure(EntityTypeBuilder<Execucao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .IsRequired();

            builder.Property(e => e.DataInicio)
                    .HasColumnName("DATA_INICIO")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(e => e.DataFim)
                    .HasColumnName("DATA_FIM")
                   .HasColumnType("datetime")
                   .IsRequired();

            //builder.Property(e => e.Logs)
            //    .HasColumnName("LOG")
            //       .HasColumnType("varchar(50)");

            builder.Property(e => e.Status)
                    .HasColumnName("STATUS")
                   .IsRequired();

            builder.Property(x => x.IdServico)
                .HasColumnName("ID_SERVICO");
            

            //builder.HasOne(x => x.Execucao)
            //    .WithMany(x => x.Servico)
            //    .HasForeignKey(x => x.IdServico)
            //    .IsRequired();
        }
    }
}
