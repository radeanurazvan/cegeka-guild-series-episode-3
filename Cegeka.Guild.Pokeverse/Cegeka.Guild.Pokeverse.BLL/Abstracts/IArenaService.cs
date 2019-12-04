using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.BLL.Models;

namespace Cegeka.Guild.Pokeverse.BLL.Abstracts
{
    public interface IArenaService
    {
        IEnumerable<FinishedBattleModel> GetFinishedBattles();

        IEnumerable<OngoingBattleModel> GetOngoingBattles();
    }
}