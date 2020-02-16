using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    public sealed class StartBattleCommand : IRequest
    {
        public StartBattleCommand(Guid attackerId, Guid defenderId)
        {
            AttackerId = attackerId;
            DefenderId = defenderId;
        }

        public Guid AttackerId { get; }

        public Guid DefenderId { get; }
    }
}