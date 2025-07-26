using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WildPokemonController : ControllerBase
	{
		private readonly IWildPokemonService _wildPokemonService;
		public WildPokemonController(IWildPokemonService wildPokemonService)
		{
			_wildPokemonService = wildPokemonService;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var wildPokemons = await _wildPokemonService.GetAllAsync();
			return Ok(wildPokemons);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var wildPokemon = await _wildPokemonService.GetByIdAsync(id);
			if (wildPokemon == null)
			{
				return NotFound();
			}
			return Ok(wildPokemon);
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] WildPokemonDto wildPokemonDto)
		{
			if (wildPokemonDto == null)
			{
				return BadRequest("Wild Pokemon data is null");
			}
			var createdWildPokemon = await _wildPokemonService.AddAsync(wildPokemonDto);
			return CreatedAtAction(nameof(GetById), new { id = createdWildPokemon.Id }, createdWildPokemon);
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] WildPokemonDto wildPokemonDto)
		{
			if (wildPokemonDto == null)
			{
				return BadRequest("Wild Pokemon data is null");
			}
			var updatedWildPokemon = await _wildPokemonService.UpdateAsync(wildPokemonDto);
			if (updatedWildPokemon == null)
			{
				return NotFound();
			}
			return Ok(updatedWildPokemon);
		}
		[HttpDelete]
		
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _wildPokemonService.DeleteAsync(id);
			if (result)
			{
				return NoContent();
			}
			return NotFound($"Wild Pokemon with id {id} not found");
		}
	}
}
