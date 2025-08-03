using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Application.Service;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WildBattleController : ControllerBase
    {
        private readonly IWildBattleService _wildBattleService;

        public WildBattleController(IWildBattleService wildBattleService)
        {
            _wildBattleService = wildBattleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var battles = await _wildBattleService.GetBattlesAsync();
            return Ok(battles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var battle = await _wildBattleService.GetByIdAsync(id);
            if (battle == null)
            {
                return NotFound();
            }
            return Ok(battle);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WildBattleDto wildBattleDto)
        {
            if (wildBattleDto == null)
            {
                return BadRequest("Battle data is null");
            }
            try
            {
                var createdBattle = await _wildBattleService.AddAsync(wildBattleDto);
                return CreatedAtAction(nameof(GetById), new { id = createdBattle.Id }, createdBattle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WildBattleDto wildBattleDto)
        {
            if (wildBattleDto == null)
            {
                return BadRequest("Battle data is null");
            }
            var updatedBattle = await _wildBattleService.UpdateAsync(wildBattleDto);
            if (updatedBattle == null)
            {
                return NotFound();
            }
            return Ok(updatedBattle);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var battle = await _wildBattleService.GetByIdAsync(id);
            if (battle == null)
            {
                return NotFound();
            }
            await _wildBattleService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost("{battleId}/turns")]
        public async Task<IActionResult> AddTurn(int battleId, [FromBody] BattleTurnDto turnDto)
        {
            if (turnDto == null)
            {
                return BadRequest("Turn data is null");
            }
            try
            {
                await _wildBattleService.AddTurnAsync(battleId, turnDto.AttackerId, turnDto.DefenderId, turnDto.BattleAction, turnDto.TurnNumber);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
