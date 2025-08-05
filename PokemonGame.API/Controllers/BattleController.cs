using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;
using System.Text.Json;

namespace PokemonGame.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;
        private readonly ILogger<BattleController> _logger;

        public BattleController(IBattleService battleService, ILogger<BattleController> logger)
        {
            _battleService = battleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var battles = await _battleService.GetBattlesAsync();
            string battlesJson =  JsonSerializer.Serialize(battles, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            _logger.LogInformation("Retrieved battles: {BattlesJson}", battlesJson);
            return Ok(battles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var battle = await _battleService.GetByIdAsync(id);
            if (battle == null)
            {
                return NotFound();
            }
            return Ok(battle);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BattleDto battleDto)
        {
            if (battleDto == null)
            {
                return BadRequest("Battle data is null");
            }
            try
            {
                var createdBattle = await _battleService.AddAsync(battleDto);
                return CreatedAtAction(nameof(GetById), new { id = createdBattle.Id }, createdBattle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BattleDto battleDto)
        {
            if (battleDto == null)
            {
                return BadRequest("Battle data is null");
            }
            var updatedBattle = await _battleService.UpdateAsync(battleDto);
            if (updatedBattle == null)
            {
                return NotFound();
            }
            return Ok(updatedBattle);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var battle = await _battleService.GetByIdAsync(id);
            if (battle == null)
            {
                return NotFound();
            }
            await _battleService.DeleteAsync(id);
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
                await _battleService.AddTurnAsync(battleId, turnDto.AttackerId, turnDto.DefenderId, turnDto.BattleAction, turnDto.TurnNumber);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
