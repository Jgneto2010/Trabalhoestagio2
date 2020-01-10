using Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace DNIT.Monitor.Api.Models
{
    public class AplicacaoModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ICollection<ServicoAplicacaoModel> Servicos { get; set; }

        public class ServicoAplicacaoModel
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
        }
    }
}
