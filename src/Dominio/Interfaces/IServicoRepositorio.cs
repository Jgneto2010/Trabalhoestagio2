﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IServicoRepositorio : IRepositorio<Servico>
    {
        Task AddExecucao(Guid idServico, Execucao execucao);
        Task<bool> Any(Guid id);
        Task<bool> Any(string nome);
        Task<Servico> Detalhar(Guid idServico);
        Task<Guid> GetByName(string nome);
        Task<Execucao> GetExecucao(Guid idExecucao);
        Task<int> SalvarLogs(IEnumerable<Logging> logs);
        Task<int> EditarExecucao(Execucao execucao);
    }
}
