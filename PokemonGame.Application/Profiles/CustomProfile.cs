using AutoMapper;
using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Application.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<Pokemon, PokemonDto>()
                .ForMember(x => x.CategoriesIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.Id).ToList()))
                .ForMember(x => x.SkillIds, opt => opt.MapFrom(src => src.Skills.Select(s => s.Id).ToList()))
                .ReverseMap();
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.PokemonIds, opt => opt.MapFrom(src => src.Pokemons.Select(p => p.Id).ToList()))
                .ReverseMap();
            CreateMap<Skill, SkillDto>()
                 .ForMember(x => x.PokemonIds, opt => opt.MapFrom(src => src.Pokemons.Select(p => p.Id).ToList()))
                .ReverseMap();
            CreateMap<Trainer, TrainerDto>()
                .ForMember(dest => dest.TrainerPokemonsIds, opt => opt.MapFrom(src => src.TrainerPokemon.Select(p => p.Id).ToList())) 
                .ReverseMap();
        }
    }
}
