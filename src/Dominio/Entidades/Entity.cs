using System;

namespace Dominio.Entidades
{
    //
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
