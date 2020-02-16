﻿using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Trainer : AggregateRoot
    {
        public string Name { get; set; }

        public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    }
}