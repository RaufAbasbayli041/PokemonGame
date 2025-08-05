using AutoMapper;
using FluentValidation;
using PokemonGame.Application.Exceptions;
using PokemonGame.Application.Validators;
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
	public class PokemonService : GenericService<Pokemon, PokemonDto>, IPokemonService
	{
		private readonly IPokemonRepository _pokemonRepository;
		private readonly IWildPokemonRepository _wildPokemonRepository;


		public PokemonService(IPokemonRepository pokemonRepository, IMapper mapper, PokemonValidator validator, IWildPokemonRepository wildPokemonRepository) : base(pokemonRepository, mapper, validator)
		{
			_pokemonRepository = pokemonRepository;
			_wildPokemonRepository = wildPokemonRepository;
		}
		public override async Task<PokemonDto> AddAsync(PokemonDto dto)
		{
			if (dto == null)
			{
				throw new ArgumentNullException(nameof(dto), "PokemonDto cannot be null");
			}

			var category = await _pokemonRepository.GetCategoriesByIdsAsync(dto.CategoriesIds);
			var foundIds = category.Select(c => c.Id).ToList();

			if (category == null || !category.Any())
			{
				throw new ArgumentException("Category not found");
			}

			var skill = await _pokemonRepository.GetSkillByIdsAsync(dto.SkillIds);
			if (skill == null || !skill.Any())
			{
				throw new ArgumentException("Skill not found");
			}

			var entity = _mapper.Map<Pokemon>(dto);
			entity.Categories = category;
			entity.Skills = skill;
			var addedData = await _pokemonRepository.AddAsync(entity);
			if (entity.IsWild == true)
			{
				if (dto.LocationId == null || dto.LocationId <= 0)
				{
					throw new ArgumentException("LocationId is required for wild Pokemon.");
				}
				var wildDto = new WildPokemonDto
				{

					PokemonId = addedData.Id,  
					Level = addedData.Level,
					LocationId = dto.LocationId.Value	,

					CurrentHP = addedData.HP
				};
				await AddWildPokemonAsync(wildDto);

			}
			var responseDto = _mapper.Map<PokemonDto>(addedData);
			return responseDto;

		}

		public async Task<bool> UploadImgAsync(int id, string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
            {
                throw new BadRequestException("File path cannot be null or empty");
            }

            var data = await _pokemonRepository.UploadImgAsyn(id,filePath);
			if (data == null)
			{
				throw new ArgumentException("Pokemon not found");
			}

			data.ImageUrl = filePath;
			var updatedData = await _pokemonRepository.UpdateAsync(data);

			return true;
		}

		public override async Task<IEnumerable<PokemonDto>> GetAllAsync()
		{
			var datas = await _pokemonRepository.GetAllAsync();
			var result = _mapper.Map<IEnumerable<PokemonDto>>(datas);
			return result;
		}

		public async Task<WildPokemonDto> AddWildPokemonAsync(WildPokemonDto dto)
		{
			var basePokemon = await _pokemonRepository.GetByIdAsync(dto.PokemonId);
			if (basePokemon == null)
			{
				throw new ArgumentException("Pokemon not found");
			}

			var wild = new WildPokemon
			{
				PokemonId = dto.PokemonId,
				Pokemon = basePokemon,
				LocationId = dto.LocationId,
				Level = dto.Level,
				HP = dto.CurrentHP,
				AppearedAt = DateTime.Now
			};
			var addedWildPokemon = await _wildPokemonRepository.AddAsync(wild);
			if (addedWildPokemon == null)
			{
				throw new ArgumentException("Wild Pokemon could not be added");
			}
			var responseDto = _mapper.Map<WildPokemonDto>(addedWildPokemon);
			return responseDto;

		}

        public override async Task<PokemonDto> UpdateAsync(PokemonDto dto)
        {
            var existing = await _pokemonRepository.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new ArgumentException("Pokemon not found");

            existing.Name = dto.Name;
            existing.HP = dto.HP;
            existing.Level = dto.Level;
            existing.ImageUrl = dto.ImageUrl;
            existing.IsWild = dto.IsWild;

            existing.PokemonBaseStats.Attack = dto.PokemonBaseStats.Attack;
            existing.PokemonBaseStats.Defense = dto.PokemonBaseStats.Defense;

            existing.Categories = await _pokemonRepository.GetCategoriesByIdsAsync(dto.CategoriesIds);
            existing.Skills = await _pokemonRepository.GetSkillByIdsAsync(dto.SkillIds);

            var updated = await _pokemonRepository.UpdateAsync(existing);
            return _mapper.Map<PokemonDto>(updated);
        }

    }


}
