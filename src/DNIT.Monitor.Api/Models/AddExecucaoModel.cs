﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIT.Monitor.Api.Models
{
    public class AddExecucaoModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Status Status { get; set; }
        public string Log { get; set; }
    }
}
