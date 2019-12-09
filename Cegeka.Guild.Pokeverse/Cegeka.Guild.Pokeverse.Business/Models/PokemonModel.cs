using System;
using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.BLL.Models
{
    public class PokemonModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AbilityModel> Abilities { get; set; }
    }
}