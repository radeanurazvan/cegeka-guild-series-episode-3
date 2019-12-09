using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Battles.Events
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