using System;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}