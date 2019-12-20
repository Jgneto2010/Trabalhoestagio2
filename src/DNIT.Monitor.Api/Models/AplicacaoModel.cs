﻿using System;
using System.Collections.Generic;

namespace DNIT.Monitor.Api.Models
{
    public class AplicacaoModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<ServicoDetalheModel> Servicos { get; set; }
    }
}
