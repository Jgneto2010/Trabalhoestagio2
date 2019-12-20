using System;
using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class Servico : Entity
    {
        public Servico(string nome)
        {
            Nome = nome;
        }

        public string Nome { get;  private set; }
        public Guid IdAplicacao { get; set; }
        public Aplicacao Aplicacao { get; set; }
        
        public ICollection<Execucao> Execucoes { get; set; }
    }
}
