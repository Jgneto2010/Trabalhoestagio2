using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IServicoRepositorio : IRepositorio<Servico>
    {
        Task AddExecucao(Guid idServico, Execucao execucao);
    }
}
