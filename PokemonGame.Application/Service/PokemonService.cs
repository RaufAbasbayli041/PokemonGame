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


        public PokemonService(IPokemonRepository pokemonRepository, IMapper mapper, PokemonValidator validator) : base(pokemonRepository,mapper,validator)
        {
            _pokemonRepository = pokemonRepository;
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

        
    }
    
    
}
