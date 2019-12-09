using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Business.Models;

namespace Cegeka.Guild.Pokeverse.Business.Abstracts
{
    public interface IArenaService
    {
        IEnumerable<FinishedBattleModel> GetFinishedBattles();

        IEnumerable<OngoingBattleModel> GetOngoingBattles();
    }
}