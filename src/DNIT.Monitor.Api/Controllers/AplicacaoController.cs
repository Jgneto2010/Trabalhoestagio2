using DNIT.Monitor.Api.Models;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;                               //-----------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DNIT.Monitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AplicacoesController : Controller
    {
        public AplicacoesController() { }
        [HttpGet]
        [Route("{nomeAplicacao}")]
        public async Task<IActionResult> Get([FromServices] IAplicacaoRepositorio repositorio, Guid id)
        {
            var result = await repositorio.Buscar(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new AplicacaoModel()
            {
                Id = result.Id,
                Nome = result.Nome,
                Servicos = result.Servicos.Select(x => new ServicoDetalheModel()
                {
                    NomeServico = x.Nome,
                })
            });
        }
        [HttpGet]
        public async Task<IEnumerable<AplicacaoModel>> Get([FromServices] IAplicacaoRepositorio repositorio)
        {
            return await repositorio.ListAll(x => new AplicacaoModel { Id = x.Id, Nome = x.Nome });
        }
        [HttpPost]
        [Route("addAplicacao")]
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, [FromBody]AddAplicacaoModel aplicacaoModel)
        {
            var aplicacao = new Aplicacao(aplicacaoModel.Nome);
            await repositorio.Add(aplicacao);
            await repositorio.SaveChanges();
            return Created($"api/aplicacao/{aplicacao.Nome}", new { aplicacao.Id, aplicacao.Nome });
        }
        
        [HttpPost("{idAplicacao:Guid}/servicos/addServico")]
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, [FromRoute] Guid idAplicacao, [FromBody]AddServicoModel servicomodel)
        {
            if (!await repositorio.Any(idAplicacao))
            {
                return NotFound();
            }
            var servico = new Servico(servicomodel.Nome);
            await repositorio.AddServico(idAplicacao, servico);
            await repositorio.SaveChanges();
            return Created($"api/aplicacoes/servicos/{servico.Id}/detalhar", new { servico.Id, servico.Nome });
        }

        [HttpGet]
        [Route("servicos/{idServico}/detalhar")]
        public async Task<IActionResult> GetDetailsServico([FromServices] IServicoRepositorio repositorio, [FromRoute]Guid idServico)
        {
            var result = await repositorio.Detalhar(idServico);
            var exec = new ServicoDetalheModel();
            exec.NomeAplicacao = result.Aplicacao.Nome;
            exec.NomeServico = result.Nome;
            exec.Id = result.Id;
            exec.ListaExecucoes = result.Execucoes;
            return Ok(exec);
        }
        [HttpPost("{nomeAplicacao}/servicos/{nomeServico}/addExecucao")]
        public async Task<IActionResult> Post([FromServices]IServicoRepositorio repositorio, [FromServices]IAplicacaoRepositorio aplicacaoRepositorio,
            Guid idServico, string nomeServico, string nomeAplicacao, [FromBody]ExecucaoModel execucaoModel)
        {
            if (!await aplicacaoRepositorio.Any(nomeAplicacao))
            {
                return NotFound();
            }
            if (!await repositorio.Any(nomeServico))
            {
                return NotFound();
            }
            var execucao = new Execucao
            {
                DataInicio = execucaoModel.DataInicio,
                DataFim = execucaoModel.DataFim,
                Log = execucaoModel.Log,
                Status = execucaoModel.Status,
                IdServico = idServico
            };
            await repositorio.AddExecucao(idServico, execucao);
            await repositorio.SaveChanges();
            return Created($"api/aplicacoes/{nomeAplicacao}/servicos/{nomeServico}/execucoes/{execucao.Id}",
                new { execucao.Id, execucao.Log, execucao.Status, execucao.DataInicio, execucao.DataFim });
        }
    }
}
