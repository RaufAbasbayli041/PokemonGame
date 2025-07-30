using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;
using PokemonGame.Persistance.DB;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PokemonGameDbContext _context;

        public PokemonController(IWebHostEnvironment webHostEnvironment, IPokemonService pokemonService, PokemonGameDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _pokemonService = pokemonService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pokemons = await _pokemonService.GetAllAsync();
            return Ok(pokemons);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }
       
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PokemonDto pokemonDto)
        {
            if (pokemonDto == null)
            {
                return BadRequest("Pokemon data is null");
            }
            var createdPokemon = await _pokemonService.AddAsync(pokemonDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPokemon.Id }, createdPokemon);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PokemonDto pokemonDto)
        {
            if (pokemonDto == null)
            {
                return BadRequest("Pokemon data is null");
            }
            var updatedPokemon = await _pokemonService.UpdateAsync(pokemonDto);
            if (updatedPokemon == null)
            {
                return NotFound();
            }
            return Ok(updatedPokemon);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _pokemonService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
      
        [HttpPost("upload-image/{id}")]
        public async Task<IActionResult> UploadImage(int id,[FromForm] FileUploadDto image)
        {
            if (image == null || image.Image.Length == 0)
            {
                return BadRequest("No image file provided");
            }
            var folder  = _webHostEnvironment.WebRootPath+ "/pokemon/images";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var fileName = $"{Guid.NewGuid()}_{image.Image.FileName}";
            var filePath = Path.Combine(folder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.Image.CopyToAsync(stream);
            }
            var isUploaded = await _pokemonService.UploadImgAsync(id, fileName);
            if (!isUploaded)
            {
                return BadRequest("Image upload failed");
            }
            return Ok ();
        }
    }
}
