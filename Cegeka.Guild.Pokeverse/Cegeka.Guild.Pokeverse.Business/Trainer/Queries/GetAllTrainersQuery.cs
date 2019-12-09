using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Business.Models;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.Queries
{
    public sealed class GetAllTrainersQuery : IRequest<IEnumerable<TrainerModel>>
    {
    }
}