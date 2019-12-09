using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business.Models;
using Cegeka.Guild.Pokeverse.Business.Trainer.Queries;
using Cegeka.Guild.Pokeverse.Domain.Abstracts;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.QueryHandlers
{
    internal sealed class GetAllTrainersQueryHandler : IRequestHandler<GetAllTrainersQuery, IEnumerable<TrainerModel>>
    {
        
        private readonly IRepository<Domain.Entities.Trainer> trainerRepository;

        public GetAllTrainersQueryHandler(IRepository<Domain.Entities.Trainer> trainerRepository)
        {
            this.trainerRepository = trainerRepository;
        }

        public Task<IEnumerable<TrainerModel>> Handle(GetAllTrainersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.trainerRepository.GetAll().Select(t => new TrainerModel
            {
                Id = t.Id,
                Name = t.Name,
                Pokemons = t.Pokemons.Select(p => new PokemonModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Abilities = p.Abilities.Select(x => new AbilityModel
                    {
                        Id = x.Id, Name = x.Name
                    }).ToList()
                }).ToList()
            }));
        }
    }
}