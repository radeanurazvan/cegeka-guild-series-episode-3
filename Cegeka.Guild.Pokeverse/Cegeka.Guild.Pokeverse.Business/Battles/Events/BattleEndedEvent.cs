using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    public sealed class BattleEndedEvent : INotification
    {
        public BattleEndedEvent(Guid battleId)
        {
            BattleId = battleId;
        }

        public Guid BattleId { get; }
    }
}