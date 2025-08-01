using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainers = await _trainerService.GetAllAsync();
            return Ok(trainers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrainerDto trainerDto)
        {
            if (trainerDto == null)
            {
                return BadRequest("Trainer data is null");
            }
            var createdTrainer = await _trainerService.AddAsync(trainerDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TrainerDto trainerDto)
        {
            if (trainerDto == null)
            {
                return BadRequest("Trainer data is null");
            }
            var updatedTrainer = await _trainerService.UpdateAsync(trainerDto);
            if (updatedTrainer == null)
            {
                return NotFound();
            }
            return Ok(updatedTrainer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _trainerService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}/trainerPokemon")]
        public async Task<IActionResult> GetTrainerPokemon(int id)
        {
            var trainerPokemon = await _trainerService.GetPokemonByTrainerIdAsync(id);
            if (trainerPokemon == null )
            {
                return NotFound($"No Pokemon found for Trainer with id {id}");
            }
            return Ok(trainerPokemon);
        }

    }
}
