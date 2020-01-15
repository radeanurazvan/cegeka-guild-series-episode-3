using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Api.Models;
using Cegeka.Guild.Pokeverse.Business.Trainer.Commands;
using Cegeka.Guild.Pokeverse.Business.Trainer.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api.Controllers
{
    [Route("api/trainers")]
    public class TrainersController : ControllerBase
    {
        private readonly IMediator mediator;

        public TrainersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.mediator.Send(new GetAllTrainersQuery()));
        }

        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody]RegisterTrainerModel model)
        {
            await this.mediator.Send(new RegisterTrainerCommand(model.Name));
            return Ok();
        }
    }
}