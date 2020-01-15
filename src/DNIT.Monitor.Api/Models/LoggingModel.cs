using System;

namespace DNIT.Monitor.Api.Models
{
    public class LoggingModel
    {
        public DateTime Hora { get; set; }
        public int Tipo { get; set; }
        public string Log { get; set; }
    }
}
