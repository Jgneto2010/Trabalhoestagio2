using Dominio.Entidades;
using Infra.Maps;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contextos
{
    public class MonitorContext : DbContext
    {
        public DbSet<Aplicacao> Aplicacao { get; set; }
        public DbSet<Execucao> Execucao { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Logging> Logging { get; set; }
        public MonitorContext(DbContextOptions<MonitorContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AplicacaoMapping());
            modelBuilder.ApplyConfiguration(new ExecucaoMapping());
            modelBuilder.ApplyConfiguration(new ServicoMapping());
            modelBuilder.ApplyConfiguration(new ServicoMapping());
            base.OnModelCreating(modelBuilder);
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Joao; Database = Mercado; User ID = sa; Password = garciajtc241188;");
        }
        */
    }
}
