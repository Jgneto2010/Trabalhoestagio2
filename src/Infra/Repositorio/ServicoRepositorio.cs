using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
    }

}
