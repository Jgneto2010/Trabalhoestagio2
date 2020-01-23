using System;

namespace Dominio.Entidades
{
    public class Logging : Entity
    {
        public DateTime Hora { get; set; }
        public TipoLog Tipo { get; set; }
        public string Log { get; set; }
        public Execucao Execucao { get; set; }
    }
}
