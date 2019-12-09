namespace Cegeka.Guild.Pokeverse.Domain.Entities
{
    public class PokemonInFight
    {
        public PokemonInFight(Pokemon pokemon)
        {
            Pokemon = pokemon;
            Health = pokemon.HealthPoints * 15;
        }

        public Pokemon Pokemon { get; set; }

        public int Health { get; set; }
    }
}