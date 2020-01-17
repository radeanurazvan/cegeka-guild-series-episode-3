using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Business.Arena.Models;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Arena.Queries
{
    public sealed class GetOngoingBattlesQuery : IRequest<IEnumerable<OngoingBattleModel>>
    {
        
    }
}