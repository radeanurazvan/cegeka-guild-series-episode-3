using System;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Business;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api
{
    [Route("api/battles")]
    public class BattlesController : ControllerBase
    {
        private readonly IMediator mediator;

        public BattlesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> StartBattle([FromBody] StartBattleModel model)
        {
            return await RunWithException(() => mediator.Send(new StartBattleCommand(model.AttackerId, model.DefenderId)), Ok);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> UseAbility([FromRoute] Guid id, [FromBody] UseAbilityModel model)
        {
            return await RunWithException(() => this.mediator.Send(new UseAbilityCommand(id, model.ParticipantId, model.AbilityId)), NoContent);
        }

        private async Task<IActionResult> RunWithException(Func<Task> act, Func<IActionResult> onOk)
        {
            try
            {
                await act();

                return onOk();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}