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
    public class TrainerService : GenericService<Trainer, TrainerDto>, ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper, IValidator<TrainerDto> validator) : base(trainerRepository, mapper, validator)
        {
            _trainerRepository = trainerRepository;
        }
        public override async Task<IEnumerable<TrainerDto>> GetAllAsync()
        {
            var datas = await _trainerRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TrainerDto>>(datas);
            return dtos;
        }

        public async Task<IEnumerable<TrainerPokemonDto>> GetPokemonByTrainerIdAsync(int trainerId)
        {
         var data = await _trainerRepository.GetPokemonByTrainerIdAsync(trainerId);
            if (data == null || !data.Any())
            {
                throw new KeyNotFoundException($"TrainerPokemon with TrainerId {trainerId} not found.");
            }
            var dto = _mapper.Map<IEnumerable<TrainerPokemonDto>>(data);
            return dto;

        }

        public override async Task<TrainerDto> GetByIdAsync(int id)
        {
            var data = await _trainerRepository.GetByIdAsync(id);
            if (data == null)
            {
                return null;
            }
            var dto = _mapper.Map<TrainerDto>(data);
            return dto;
        }
    }
}
