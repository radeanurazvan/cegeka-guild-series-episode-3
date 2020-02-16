using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class RegisterTrainerCommandHandler : IRequestHandler<RegisterTrainerCommand>
    {
        private readonly IWriteRepository<Trainer> trainerReadRepository;
        private readonly IMediator mediator;

        public RegisterTrainerCommandHandler(IWriteRepository<Trainer> trainerReadRepository, IMediator mediator)
        {
            this.trainerReadRepository = trainerReadRepository;
            this.mediator = mediator;
        }

        public Task<Unit> Handle(RegisterTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainer = new Trainer { Name = request.Name };

            this.trainerReadRepository.Add(trainer);
            this.trainerReadRepository.Save();
            this.mediator.Publish(new TrainerRegisteredEvent(trainer.Id), cancellationToken);
            return Task.FromResult(Unit.Value);
        }
    }
}