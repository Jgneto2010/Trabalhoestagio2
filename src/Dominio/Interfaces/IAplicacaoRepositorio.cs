using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IAplicacaoRepositorio : IRepositorio<Aplicacao>
    {
        Task AddServico(Guid idAplicacao, Servico servico);
        Task<bool> Any(string nome);
        Task<bool> Any(Guid id);
        Task<Aplicacao> Buscar(Guid id);

        Task<List<TResult>> ListAll<TResult>(Expression<Func<Aplicacao, TResult>> selector);


    }
}
