using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Trainer.Commands;
using Cegeka.Guild.Pokeverse.Business.Trainer.Events;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.CommandHandlers
{
    internal sealed class RegisterTrainerCommandHandler : IRequestHandler<RegisterTrainerCommand>
    {
        private readonly IWriteRepository<Domain.Entities.Trainer> trainerReadRepository;
        private readonly IMediator mediator;

        public RegisterTrainerCommandHandler(IWriteRepository<Domain.Entities.Trainer> trainerReadRepository, IMediator mediator)
        {
            this.trainerReadRepository = trainerReadRepository;
            this.mediator = mediator;
        }

        public Task<Unit> Handle(RegisterTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainer = new Domain.Entities.Trainer { Name = request.Name };

            this.trainerReadRepository.Add(trainer);
            this.trainerReadRepository.Save();
            this.mediator.Publish(new TrainerRegisteredEvent(trainer.Id), cancellationToken);
            return Task.FromResult(Unit.Value);
        }
    }
}