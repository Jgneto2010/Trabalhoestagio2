using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class AplicacaoRepositorio : RepositorioBase<Aplicacao>, IRepositorio<Aplicacao>, IAplicacaoRepositorio
    {
        private readonly MonitorContext _contexto;

        public AplicacaoRepositorio(MonitorContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        
        public async Task AddServico(Guid idAplicacao,Servico servico)
        {
            var result = await DbSet.FirstOrDefaultAsync(x => x.Id == idAplicacao);
            servico.Aplicacao = result;
            _contexto.Servico.Add(servico);
            await _contexto.SaveChangesAsync();
        }

        
        public Task<bool> Any(string nome)
        {
            return DbSet.AnyAsync(x => x.Nome == nome);
        }

        
        public Task<bool> Any(Guid id)
        {
            return DbSet.AnyAsync(x => x.Id == id);
        }

        
        public Task<Aplicacao> Buscar(Guid id)
        {
            return DbSet.Include(x => x.Servicos).FirstOrDefaultAsync(x => x.Id == id);
        }

        
        public IEnumerable<Aplicacao> GetAll()
        {
            return _contexto.Aplicacao.ToList();
        }

       
        public Task<List<TResult>> ListAll<TResult>(Expression<Func<Aplicacao, TResult>> selector)
        {
            return DbSet.Select(selector).ToListAsync();
        }
    }
}
