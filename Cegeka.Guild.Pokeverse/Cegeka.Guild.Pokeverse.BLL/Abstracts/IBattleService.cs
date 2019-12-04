using System;

namespace Cegeka.Guild.Pokeverse.BLL.Abstracts
{
    public interface IBattleService
    {
        void StartBattle(Guid attackerId, Guid defenderId);

        void UseAbility(Guid battleId, Guid participantId, Guid abilityId);
    }
}