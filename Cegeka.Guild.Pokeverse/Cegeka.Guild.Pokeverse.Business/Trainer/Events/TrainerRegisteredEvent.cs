using System;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.Events
{
    public sealed class TrainerRegisteredEvent : INotification
    {
        private TrainerRegisteredEvent(){ }

        public TrainerRegisteredEvent(Guid id)
            : this()
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}