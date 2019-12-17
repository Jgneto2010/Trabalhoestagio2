using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class Aplicacao : Entity
    {
        public Aplicacao(string nome)
        {
            Nome = nome;
        }
        public string Nome { get; private set; }
        public ICollection<Servico> Servicos { get; set; }
    }
}
