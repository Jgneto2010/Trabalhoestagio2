﻿using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class ServicoRepositorio : RepositorioBase<Servico>, IRepositorio<Servico>, IServicoRepositorio
    {
        private readonly MonitorContext _contexto;
        public ServicoRepositorio(MonitorContext contexto) : base(contexto)
        {
            _contexto = contexto;

        }
        public Task<bool> Any(Guid id)
        {
            return DbSet.AnyAsync(x => x.Id == id);
        }
        public Task<bool> Any(string nome)
        {
            return DbSet.AnyAsync(x => x.Nome == nome);
        }
        public Task<Servico> Detalhar(Guid idServico)
        {
            return DbSet.Include(i => i.Aplicacao).Include(e => e.Execucoes).FirstOrDefaultAsync(x => x.Id == idServico);
        }
        //Esse metodo traz a lista de execuçoes contidas em serviços
        public async Task AddExecucao(Guid idServico, Execucao execucao)
        {
            //Aqui está indo no banco trazendo o objeto serviço
            var result = await DbSet.FirstOrDefaultAsync(x => x.Id == idServico);
            execucao.Servico = result;
            //Aqui ele insere uma execução na lista de execuçoes contidas no Serviço
            _contexto.Execucao.Add(execucao);
            await _contexto.SaveChangesAsync();
        }
        public async Task<Guid> GetByName(string nome)
        {
            var result = await _contexto.Servico.FirstOrDefaultAsync(x => x.Nome == nome);
            return result.Id;
        }

        public Task<Execucao> GetExecucao(Guid idExecucao)
        {
            return _contexto.Execucao.FirstOrDefaultAsync(x => x.Id == idExecucao);
        }

        public Task<int> SalvarLogs(IEnumerable<Logging> logs)
        {
            _contexto.Logging.AddRange(logs);
            return _contexto.SaveChangesAsync();
        }

        public Task<int> EditarExecucao(Execucao execucao)
        {
            _contexto.Entry(execucao).State = EntityState.Modified;
            return _contexto.SaveChangesAsync();
        }

        
    }

}
