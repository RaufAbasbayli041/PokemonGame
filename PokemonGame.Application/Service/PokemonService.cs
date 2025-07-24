using AutoMapper;
using FluentValidation;
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


		public PokemonService(IPokemonRepository pokemonRepository, IMapper mapper, PokemonValidator validator) : base(pokemonRepository, mapper, validator)
		{
			_pokemonRepository = pokemonRepository;
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
			var entity = _mapper.Map<Pokemon>(dto);
			entity.Categories = category;
			entity.Skills = skill;
			var addedData = await _pokemonRepository.AddAsync(entity);
			var responseDto = _mapper.Map<PokemonDto>(addedData);
			return responseDto;

		}

		public async Task<bool> UploadImgAsync(int id, string filePath)
		{
			var data = await _pokemonRepository.GetByIdAsync(id);
			if (data == null)
			{
				throw new ArgumentException("Pokemon not found");
			}
			var isUploaded = await _pokemonRepository.UploadImgAsyn(id, filePath);
			if (isUploaded == null)
			{
				return false;
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

	}


}
