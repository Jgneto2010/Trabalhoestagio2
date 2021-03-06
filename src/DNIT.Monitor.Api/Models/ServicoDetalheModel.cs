﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIT.Monitor.Api.Models
{
    public class ServicoDetalheModel
    {
        public Guid Id { get; set; }
        public string NomeAplicacao { get; set; }
        public string NomeServico { get; set; }
        public ICollection<ExecucaoModel> ListaExecucoes { get; set; }
    }
}
