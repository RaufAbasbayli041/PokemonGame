using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Dtos
{
    public record   SkillDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PokemonDto> Pokemons { get; set; } = new List<PokemonDto>();
    }
}
