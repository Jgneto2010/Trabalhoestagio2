using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public abstract class RepositorioBase<T> : IRepositorio<T>
        where T : Entity
    {

        private readonly MonitorContext _contexto;
        protected readonly DbSet<T> DbSet;
        public RepositorioBase(MonitorContext contexto)
        {
            _contexto = contexto;
            DbSet = _contexto.Set<T>();
        }

        public ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> Add(T obj)
        {
            return DbSet.AddAsync(obj);
        }

        public Task<List<T>> GetAll()
        {
            return DbSet.ToListAsync();
        }

        public Task<T> GetById(Guid id)
        {
            return DbSet.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task Remove(Guid id)
        {
            var aplicacao = await GetById(id);
            DbSet.Remove(aplicacao);
        }

        public Task<int> SaveChanges()
        {
            return _contexto.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            DbSet.Update(obj);
        }
        
    }
}
