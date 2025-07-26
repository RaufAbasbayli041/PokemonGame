using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _locationService.GetAllAsync();
            return Ok(locations);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] LocationDto locationDto)
        {
            if (locationDto == null)
            {
                return BadRequest("Location data is null");
            }
            var createdLocation = await _locationService.AddAsync(locationDto);
            return CreatedAtAction(nameof(GetById), new { id = createdLocation.Id }, createdLocation);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LocationDto locationDto)
        {
            if (locationDto == null)
            {
                return BadRequest("Location data is invalid");
            }
            var updatedLocation = await _locationService.UpdateAsync(locationDto);
            if (updatedLocation == null)
            {
                return NotFound();
            }
            return Ok(updatedLocation);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _locationService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
