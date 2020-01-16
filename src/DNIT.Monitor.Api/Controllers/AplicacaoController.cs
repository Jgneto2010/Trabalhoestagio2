using DNIT.Monitor.Api.Models;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIT.Monitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AplicacoesController : Controller
    {
        public AplicacoesController() { }

        [HttpGet]
        [Route("{nomeAplicacao}")]
        public async Task<IActionResult> Get([FromServices] IAplicacaoRepositorio repositorio, string nomeAplicacao)
        {
            var result = await repositorio.GetByName(nomeAplicacao);

            if (result == default)
                return NotFound();

            return Ok(new AplicacaoModel
            {
                Id = result.Id,
                Nome = result.Nome,
                Servicos = result
                    .Servicos
                    .Select(x => new AplicacaoModel.ServicoAplicacaoModel { Id = x.Id, Nome = x.Nome }).ToList()
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
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, [FromRoute] Guid idAplicacao, [FromBody]AddServicoModel addServicomodel)
        {
            if (!await repositorio.Any(idAplicacao))
                return NotFound();

            var servico = new Servico(addServicomodel.Nome);
            await repositorio.AddServico(idAplicacao, servico);
            await repositorio.SaveChanges();
            return Created($"api/aplicacoes/{idAplicacao}/servicos/{servico.Id}/detalhar", new { servico.Id, servico.Nome });
        }

        [HttpGet]
        [Route("servicos/{idServico}/detalhar")]
        public async Task<IActionResult> GetDetailsServico([FromServices] IServicoRepositorio repositorio, [FromRoute]Guid idServico)
        {
            if (!await repositorio.Any(idServico))
            {
                return NotFound();
            }

            var servicoEntidade = await repositorio.Detalhar(idServico);

            var execucoes = servicoEntidade.Execucoes.Select(execucao => new ExecucaoModel
            {
                Id = execucao.Id,
                DataInicio = execucao.DataInicio,
                DataFim = execucao.DataFim,
                TextoStatus = Enum.GetName(typeof(Status), execucao.Status)
            });


            var servicoModel = new ServicoModel
            {
                ListaExecucoes = execucoes.ToList(),
                NomeAplicacao = servicoEntidade.Aplicacao.Nome,
                Nome = servicoEntidade.Nome,
                Id = servicoEntidade.Id
            };

            return Ok(servicoModel);
        }

        [HttpPost("{nomeAplicacao}/servicos/{nomeServico}/addExecucao")]
        public async Task<IActionResult> Post([FromServices]IServicoRepositorio repositorio, [FromServices]IAplicacaoRepositorio aplicacaoRepositorio,
             string nomeAplicacao, string nomeServico, [FromBody]AddExecucaoModel addExecucaoModel)
        {
            if (!await aplicacaoRepositorio.Any(nomeAplicacao))
                return NotFound();

            if (!await repositorio.Any(nomeServico))
                return NotFound();

            var execucao = new Execucao
            {
                DataInicio = DateTime.Now,
                Status = Status.Executando,
                IdServico = repositorio.GetByName(nomeServico).Result
            };

            await repositorio.AddExecucao(repositorio.GetByName(nomeServico).Result, execucao);

            await repositorio.SaveChanges();
            return Created($"api/aplicacoes/{nomeAplicacao}/servicos/{nomeServico}/execucoes/{execucao.Id}",
                new { execucao.Id, execucao.DataInicio });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositorio"></param>
        /// <param name="idExecucao"></param>
        /// <param name="logs"></param>
        /// <returns></returns>
        [HttpPost("servicos/{idExecucao}/finalizarExecucao")]
        public async Task<IActionResult> PostFinalizarExecucao([FromServices]IServicoRepositorio repositorio, [FromRoute] Guid idExecucao, [FromBody] List<LoggingModel> logs)
        {
            var execucao = await repositorio.GetExecucao(idExecucao);

            if (execucao == default)
            {
                return NotFound();
            }

            execucao.DataFim = DateTime.Now;
            execucao.Status = logs.Any(x => x.Tipo == (int)TipoLog.Erro) ? Status.Erro : Status.Finalizado;

            await repositorio.EditarExecucao(execucao);

            var loggins = logs.Select(x => new Logging
            {
                Hora = x.Hora,
                Log = x.Log,
                Tipo = (TipoLog)x.Tipo
            });

            await repositorio.SalvarLogs(loggins);

            return Ok();
        }
    }
}

