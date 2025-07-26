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
    public class GymService : GenericService<Gym, GymDto>, IGymService
    {
        private readonly IGymRepository _gymRepository;
        public GymService(IGymRepository repository, IMapper mapper, GymValidator validator) : base(repository, mapper, validator)
        {
            _gymRepository = repository;
        }
    }
}
