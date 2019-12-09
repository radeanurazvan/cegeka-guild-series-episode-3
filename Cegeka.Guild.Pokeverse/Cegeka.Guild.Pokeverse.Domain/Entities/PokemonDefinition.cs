using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.DAL.Entities
{
    public class PokemonDefinition : Entity
    {
        public string Name { get; set; }

        public ICollection<Ability> Abilities { get; set; }
    }
}