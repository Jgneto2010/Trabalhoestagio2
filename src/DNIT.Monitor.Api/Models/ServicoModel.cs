using System;
using System.Collections.Generic;

namespace DNIT.Monitor.Api.Models
{
    public class ServicoModel
    {
        public ServicoModel()
        {
        }
        public Guid Id { get; set; }
        public string NomeAplicacao { get; set; }
        public string Nome { get; set; }
        public ICollection<ExecucaoModel> ListaExecucoes { get; set; }
    }
}
