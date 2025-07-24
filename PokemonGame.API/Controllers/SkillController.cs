using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonGame.Application.Service;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _skillService.GetAllAsync();
            return Ok(skills);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SkillDto skillDto)
        {
            if (skillDto == null)
            {
                return BadRequest("Skill data is null");
            }
            var createSkill = await _skillService.AddAsync(skillDto);
            return CreatedAtAction(nameof(GetById), new { id = createSkill.Id }, createSkill);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SkillDto skillDto)
        {
            if (skillDto == null)
            {
                return BadRequest("Skill data is null");
            }
            var updatedSkill = await _skillService.UpdateAsync(skillDto);
            if (updatedSkill == null)
            {
                return NotFound();
            }
            return Ok(updatedSkill);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _skillService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();

        }

    }
}
