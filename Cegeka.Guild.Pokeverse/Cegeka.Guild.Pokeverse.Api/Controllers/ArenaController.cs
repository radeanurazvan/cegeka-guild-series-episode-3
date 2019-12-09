using Cegeka.Guild.Pokeverse.Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api.Controllers
{
    [Route("api/arena")]
    public class ArenaController : ControllerBase
    {
        private readonly IArenaService arenaService;

        public ArenaController(IArenaService arenaService)
        {
            this.arenaService = arenaService;
        }

        [HttpGet("ongoing")]
        public IActionResult GetOngoingBattles()
        {
            var battles = this.arenaService.GetOngoingBattles();
            return Ok(battles);
        }


        [HttpGet("finished")]
        public IActionResult GetFinishedBattles()
        {
            var battles = this.arenaService.GetFinishedBattles();
            return Ok(battles);
        }
    }
}