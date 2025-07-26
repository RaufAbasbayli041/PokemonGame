using AutoMapper;
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
    public class LocationService : GenericService<Location, LocationDto>, ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository, IMapper mapper, LocationValidator validator) : base(locationRepository, mapper, validator)
        {
            _locationRepository = locationRepository;
        }
        public override async Task<IEnumerable<LocationDto>> GetAllAsync()
        {
            var datas = await _locationRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<LocationDto>>(datas);
            return dtos;
        }
    }
}
