using DNIT.Monitor.Api.Models;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DNIT.Monitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AplicacaoController : Controller
    {
        public AplicacaoController() { }

        //Aqui um Metodo Get que Busca Uma Aplicação dado o seu Id
        [HttpGet]
        [Route("{id:Guid}")]
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
                    NomeServico = x.Nome
                })
            });

        }
        
        //EsteMetodo Traz todas as Aplicaçoes
        [HttpGet]
        public async Task<IEnumerable<AplicacaoModel>> Get([FromServices] IAplicacaoRepositorio repositorio)
        {
            return await repositorio.ListAll(x => new AplicacaoModel {Id = x.Id, Nome = x.Nome });
        }


        //Aqui um Metodo para salvar a aplicação, retorna um Id da aplicação, o nome e a lista de serviços da aplicação
        [HttpPost]
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, [FromBody]AddAplicacaoModel aplicacaoModel)
        {
            var aplicacao = new Aplicacao(aplicacaoModel.Nome);
            await repositorio.Add(aplicacao);
            await repositorio.SaveChanges();
            return Created($"api/aplicacao/{aplicacao.Id}", new { aplicacao.Id, aplicacao.Nome});
        }

        
        //Aqui um metodo para Salvar Um Serviço que será inserido na aplicação
        [HttpPost("{idAplicacao:Guid}/addServico")]
        public async Task<IActionResult> Post([FromServices]IAplicacaoRepositorio repositorio, Guid idAplicacao, [FromBody]AddServicoModel servicomodel)
        {
            //Aqui uma condição a qual só se cria um serviço se haver um Id de Aplicação
            //Aqui a função ANY criada no repositorio e na Interface
            //Retorna O Id do serviço e Nome
            if(!await repositorio.Any(idAplicacao))
            {
                return NotFound();
            }
            
            var servico = new Servico(servicomodel.Nome);


            await repositorio.AddServico(idAplicacao, servico);
            await repositorio.SaveChanges();
            return Created($"api/aplicacao/{servico.Id}", new { servico.Id, servico.Nome });
        }


        [HttpPost("{idServico:Guid}/addExecucao")]
        public async Task<IActionResult> Post([FromServices]IServicoRepositorio repositorio, Guid idServico, [FromBody]AddExecucaoModel execucaoModel)
        {
            //Aqui uma condição a qual só se cria um serviço se haver um Id de Aplicação
            //Aqui a função ANY criada no repositorio e na Interface
            //Retorna O Id do serviço e Nome
            //if (!await repositorio.Any(idServico))
            //{
            //    return NotFound();
            //}

            var execucao = new Execucao();
            execucao.DataInicio = execucaoModel.DataInicio;
            execucao.DataFim = execucaoModel.DataFim;
            execucao.Log = execucaoModel.Log;
            execucao.Status = execucaoModel.Status;
            execucao.IdServico = idServico;

            await repositorio.AddExecucao(idServico, execucao);

            await repositorio.SaveChanges();
            return Created($"api/aplicacao/{execucao.Id}", new { execucao.Id, execucao.Log, execucao.Status });
        }


    }
}
