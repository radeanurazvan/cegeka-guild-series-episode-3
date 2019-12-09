using System;

namespace Cegeka.Guild.Pokeverse.Business.Abstracts
{
    public interface IBattleService
    {
        void StartBattle(Guid attackerId, Guid defenderId);

        void UseAbility(Guid battleId, Guid participantId, Guid abilityId);
    }
}