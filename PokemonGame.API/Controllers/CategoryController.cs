using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Application.Service;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is null");
            }
            var createdCategory = await _categoryService.AddAsync(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is null");
            }
            var updatedCategory = await _categoryService.UpdateAsync(categoryDto);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();

        }
        
    }
}
