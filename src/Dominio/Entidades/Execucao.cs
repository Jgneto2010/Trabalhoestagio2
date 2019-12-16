using System;

namespace Dominio.Entidades
{
    public class Execucao : Entity
    {
        public Guid IdServico { get; set; }
        public DateTime DataInicio { get;  set; }
        public DateTime DataFim { get;  set; }
        public Status Status { get;  set; }
        public string Log { get; set; }

     
    }
}
