using Cegeka.Guild.Pokeverse.Api.Models;
using Cegeka.Guild.Pokeverse.Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api.Controllers
{
    [Route("api/trainers")]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService trainersService;

        public TrainersController(ITrainerService trainersService)
        {
            this.trainersService = trainersService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.trainersService.GetAll());
        }

        [HttpPost("")]
        public IActionResult Register([FromBody]RegisterTrainerModel model)
        {
            this.trainersService.Register(model.Name);
            return Ok();
        }
    }
}