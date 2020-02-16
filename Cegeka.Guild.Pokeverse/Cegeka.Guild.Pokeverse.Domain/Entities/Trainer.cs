using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Trainer : Entity
    {
        public string Name { get; set; }

        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    }
}