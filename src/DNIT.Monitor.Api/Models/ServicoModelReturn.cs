using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIT.Monitor.Api.Models
{
    public class ServicoModelReturn
    {
        //result.Nome, result.Id, Aplicacao = result.Aplicacao.Nome, result.Execucoes

        public Guid Id { get; set; }
        public string NomeAplicacao { get; set; }
        public string NomeServico { get; set; }

        public ICollection<Execucao> ListaExecucoes { get; set; }


    }
}
