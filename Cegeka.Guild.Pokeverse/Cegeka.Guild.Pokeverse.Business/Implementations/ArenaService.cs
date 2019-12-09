using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Business.Abstracts;
using Cegeka.Guild.Pokeverse.Business.Models;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using Cegeka.Guild.Pokeverse.Domain.Entities;

namespace Cegeka.Guild.Pokeverse.Business.Implementations
{
    internal class ArenaService : IArenaService
    {
        private readonly IRepository<Battle> battleRepository;

        public ArenaService(IRepository<Battle> battleRepository)
        {
            this.battleRepository = battleRepository;
        }

        public IEnumerable<FinishedBattleModel> GetFinishedBattles()
        {
            return battleRepository.GetAll()
                .Where(b => b.Winner != null)
                .Select(b => new FinishedBattleModel
                {
                    Attacker = b.Attacker.Pokemon.Name,
                    Defender = b.Defender.Pokemon.Name,
                    StartedAt = b.StartedAt,
                    FinishedAt = b.FinishedAt,
                    Winner = b.Winner.Name
                });
        }

        public IEnumerable<OngoingBattleModel> GetOngoingBattles()
        {
            return battleRepository.GetAll()
                .Where(b => b.Winner == null)
                .Select(b => new OngoingBattleModel
                {
                    Id = b.Id,
                    Attacker = b.Attacker.Pokemon.Name,
                    AttackerHealth = b.Attacker.Health,
                    Defender = b.Defender.Pokemon.Name,
                    DefenderHealth = b.Defender.Health,
                    StartedAt = b.StartedAt
                });
        }
    }
}