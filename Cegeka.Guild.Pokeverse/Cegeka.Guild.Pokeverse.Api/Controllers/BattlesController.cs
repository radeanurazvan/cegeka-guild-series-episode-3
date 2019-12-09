using System;
using Cegeka.Guild.Pokeverse.Api.Models;
using Cegeka.Guild.Pokeverse.Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Guild.Pokeverse.Api.Controllers
{
    public class BattlesController : ControllerBase
    {
        private readonly IBattleService battleService;

        public BattlesController(IBattleService battleService)
        {
            this.battleService = battleService;
        }

        [HttpPost("")]
        public IActionResult StartBattle([FromBody] StartBattleModel model)
        {
            return RunWithException(() => this.battleService.StartBattle(model.AttackerId, model.DefenderId), Ok);
        }

        [HttpPatch("{id:Guid}")]
        public IActionResult UseAbility([FromRoute] Guid id, [FromBody] UseAbilityModel model)
        {
            return RunWithException(() => this.battleService.UseAbility(id, model.ParticipantId, model.AbilityId), NoContent);
        }

        private IActionResult RunWithException(Action act, Func<IActionResult> onOk)
        {
            try
            {
                act();
                return onOk();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}