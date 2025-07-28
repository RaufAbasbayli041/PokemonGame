using AutoMapper;
using FluentValidation;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Service
{
    public class SkillService : GenericService<Skill, SkillDto>, ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository, IMapper mapper, IValidator<SkillDto> validator)
           : base(skillRepository, mapper, validator)
        {
            _skillRepository = skillRepository;
        }

        public override async Task<IEnumerable<SkillDto>> GetAllAsync()
        {
            var skills = await _skillRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SkillDto>>(skills);
        }

        //public override async Task<SkillDto> AddAsync(SkillDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ArgumentNullException(nameof(dto), "PokemonDto cannot be null");
        //    }
        //    var pokemon = await _skillRepository.GetPokemonsByIds(dto.PokemonIds);
        //    var foundIds = pokemon.Select(c => c.Id).ToList();

        //    if (pokemon == null || !pokemon.Any())
        //    {
        //        throw new ArgumentException("pokemon not found");
        //    }

        //    var entity = _mapper.Map<Skill>(dto);
        //    entity.Pokemons = pokemon;

        //    var addedData = await _skillRepository.AddAsync(entity);
        //    var responseDto = _mapper.Map<SkillDto>(addedData);
        //    return responseDto;

        //}
    }
}
