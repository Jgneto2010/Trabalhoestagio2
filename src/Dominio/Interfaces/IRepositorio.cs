using Dominio.Entidades;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IRepositorio<T>
       where T : Entity
    {
        ValueTask<EntityEntry<T>> Add(T obj);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        void Update(T obj);
        Task Remove(Guid id);
        Task<int> SaveChanges();
    }
}
