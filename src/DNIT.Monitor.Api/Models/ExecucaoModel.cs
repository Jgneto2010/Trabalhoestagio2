using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DNIT.Monitor.Api.Models
{

    public class ExecucaoModel
    {
        public Guid Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }  
        [JsonIgnore]
        public Status Status { get; set; }
        public string TextoStatus { get; set; }
        //Testes
        public ICollection<string> Logs { get; set; }
       
    }

   
}
