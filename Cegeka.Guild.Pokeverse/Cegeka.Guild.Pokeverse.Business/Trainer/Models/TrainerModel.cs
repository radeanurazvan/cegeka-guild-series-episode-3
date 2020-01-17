﻿using System;
using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.Models
{
    public class TrainerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<PokemonModel> Pokemons { get; set; }
    }
}