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
    public class TrainerPokemonService : GenericService<TrainerPokemon, TrainerPokemonDto>, ITrainerPokemonService
    {
        private readonly ITrainerPokemonRepository _trainerPokemonRepository;

        public TrainerPokemonService(ITrainerPokemonRepository trainerPokemonRepository, IMapper mapper, TrainerPokemonValidator validator) : base(trainerPokemonRepository, mapper, validator)
        {
            _trainerPokemonRepository = trainerPokemonRepository;
        }

        public override async Task<IEnumerable<TrainerPokemonDto>> GetAllAsync()
        {
            var datas = await _trainerPokemonRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TrainerPokemonDto>>(datas);
            return dtos;
        }

        public override async Task<TrainerPokemonDto> AddAsync(TrainerPokemonDto dto)
        {
            var entity = _mapper.Map<TrainerPokemon>(dto);
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "TrainerPokemonDto cannot be null");
            }
            var trainerPOkeomon = await _trainerPokemonRepository.AddAsync(entity);
            if (trainerPOkeomon == null)
            {
                throw new ArgumentException("TrainerPokemon not found");
            }
            var responseDto = _mapper.Map<TrainerPokemonDto>(trainerPOkeomon);
            return responseDto;

        }
    }
}
