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

        //Esse metodo traz a lista de execuçoes contidas em serviços
        public async Task AddExecucao(Guid idServico, Execucao execucao)
        {
            //Aqui está indo no banco trazendo o objeto serviço
            var result = await DbSet.FirstOrDefaultAsync(x => x.Id == idServico);
            result.Execucoes = new List<Execucao>();
            //Aqui ele insere uma execução na lista de execuçoes contidas no Serviço
            result.Execucoes.Add(execucao);
            _contexto.Servico.Update(result);
            await _contexto.SaveChangesAsync();
        }
    }
    
}
