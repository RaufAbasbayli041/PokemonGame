using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerPokemonController : ControllerBase
    {
        private readonly ITrainerPokemonService _trainerPokemonService;

        public TrainerPokemonController(ITrainerPokemonService trainerPokemonService)
        {
            _trainerPokemonService = trainerPokemonService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _trainerPokemonService.GetAllAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(TrainerPokemonDto dto)
        {
            if (dto == null)
            {
                return BadRequest("TrainerPokemonDto cannot be null");
            }
            var result = await _trainerPokemonService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _trainerPokemonService.DeleteAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound($"TrainerPokemon with id {id} not found");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TrainerPokemonDto dto)
        {
            if (dto == null)
            {
                return BadRequest("TrainerPokemonDto cannot be null");
            }
            var result = await _trainerPokemonService.UpdateAsync(dto);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"TrainerPokemon with id {dto.Id} not found");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _trainerPokemonService.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"TrainerPokemon with id {id} not found");
        }
        
    }
}
