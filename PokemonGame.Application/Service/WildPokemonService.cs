﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using PokemonGame.Application.Validators;
using PokemonGame.Contracts.Contracts;
using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;
using PokemonGame_Domain.Repository;

namespace PokemonGame.Application.Service
{
	public class WildPokemonService : GenericService<WildPokemon, WildPokemonDto>, IWildPokemonService
	{
		private readonly IWildPokemonRepository _wildPokemonRepository;

		public WildPokemonService(IWildPokemonRepository repository, IMapper mapper, WildPokemonValidator validator) : base(repository, mapper, validator)
		{
			_wildPokemonRepository = repository;
		}

		public override async Task<IEnumerable<WildPokemonDto>> GetAllAsync()
		{
			var datas = await _wildPokemonRepository.GetAllAsync();
			if (datas == null || !datas.Any())
			{
				return Enumerable.Empty<WildPokemonDto>();
			}
			var wildPokemonDtos = _mapper.Map<IEnumerable<WildPokemonDto>>(datas);
			return wildPokemonDtos;

		}
	}
}
