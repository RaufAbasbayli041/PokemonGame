using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;
        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gyms = await _gymService.GetAllAsync();
            return Ok(gyms);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gym = await _gymService.GetByIdAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            return Ok(gym);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GymDto gymDto)
        {
            if (gymDto == null)
            {
                return BadRequest("Gym data is null");
            }
            var createdGym = await _gymService.AddAsync(gymDto);
            return CreatedAtAction(nameof(GetById), new { id = createdGym.Id }, createdGym);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GymDto gymDto)
        {
            if (gymDto == null)
            {
                return BadRequest("Gym data is null");
            }
            var updatedGym = await _gymService.UpdateAsync(gymDto);
            if (updatedGym == null)
            {
                return NotFound();
            }
            return Ok(updatedGym);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _gymService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
