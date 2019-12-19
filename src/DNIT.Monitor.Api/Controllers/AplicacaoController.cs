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

        //Aqui um Metodo Get que Busca Uma Aplicação dado o seu Id e os serviços associados
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
                Servicos = result.Servicos.Select(x => new AplicacaoDetalheModel()
                {
                    IdServico = x.Id,
                    NomeServico = x.Nome,
                })
            });

        }

        //EsteMetodo Traz todas as Aplicaçoes
        [HttpGet]
        public async Task<IEnumerable<AplicacaoModel>> Get([FromServices] IAplicacaoRepositorio repositorio)
        {
            return await repositorio.ListAll(x => new AplicacaoModel { Id = x.Id, Nome = x.Nome });
        }

        /// <summary>
        /// Aqui um Metodo para salvar a aplicação, retorna um Id da aplicação, o nome
        /// </summary>
        /// <param name="repositorio"></param>
        /// <param name="aplicacaoModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addAplicacao")]
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, [FromBody]AddAplicacaoModel aplicacaoModel)
        {
            var aplicacao = new Aplicacao(aplicacaoModel.Nome);
            await repositorio.Add(aplicacao);
            await repositorio.SaveChanges();
            return Created($"api/aplicacao/{aplicacao.Nome}", new { aplicacao.Id, aplicacao.Nome });
        }


        //Aqui um metodo para Salvar Um Serviço que será inserido na aplicação
        [HttpPost("{idAplicacao:Guid}/servicos/addServico")]
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, [FromRoute] Guid idAplicacao, [FromBody]AddServicoModel servicomodel)
        {
            //Aqui uma condição a qual só se cria um serviço se haver um Id de Aplicação
            //Aqui a função ANY criada no repositorio e na Interface
            //Retorna O Id do serviço e Nome
            if (!await repositorio.Any(idAplicacao))
            {
                return NotFound();
            }

            var servico = new Servico(servicomodel.Nome);
            await repositorio.AddServico(idAplicacao, servico);
            await repositorio.SaveChanges();
            return Created($"api/aplicacoes/servicos/{servico.Id}/detalhar", new { servico.Id, servico.Nome });
        }
        //implementaçoes
        [HttpGet]
        [Route("servicos/{idServico}/detalhar")]
        public async Task<IActionResult> GetDetailsServico([FromServices] IServicoRepositorio repositorio, [FromRoute]Guid idServico)
        {

            var result = await repositorio.Detalhar(idServico);
           
            var exec = new ServicoModelReturn();


            exec.NomeAplicacao = result.Aplicacao.Nome;
            exec.NomeServico = result.Nome;
            exec.Id = result.Id;

            exec.ListaExecucoes = result.Execucoes;
            

            return Ok(exec);
        }


        //Aqui um metodo que cria uma execução dado Id de um serviço
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
