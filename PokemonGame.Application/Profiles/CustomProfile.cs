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
            CreateMap<TrainerPokemon, TrainerPokemonDto>().ReverseMap();
            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.GymsIds, opt => opt.MapFrom(src => src.Gyms.Select(g => g.Id).ToList()))
                .ForMember(dest => dest.WildPokemonsIds, opt => opt.MapFrom(src => src.WildPokemons.Select(wp => wp.Id).ToList()))
                .ForMember(dest => dest.WildBattlesIds, opt => opt.MapFrom(src => src.WildBattles.Select(wb => wb.Id).ToList()))
                .ReverseMap();
            CreateMap<Gym, GymDto>()
                .ForMember(dest => dest.BattlesIds, opt => opt.MapFrom(src => src.Battles.Select(w=>w.Id).ToList()))
                .ReverseMap();
            CreateMap<WildPokemon, WildPokemonDto>()
                .ForMember(dest => dest.LocationId, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Battle, BattleDto>()
                .ForMember(dest=>dest.TrainerPokemon1Id, opt => opt.MapFrom(src => src.TrainerPokemon1Id))
                .ForMember(dest => dest.TrainerPokemon2Id, opt => opt.MapFrom(src => src.TrainerPokemon2Id))
                .ForMember(dest=>dest.WinnerId, opt => opt.MapFrom(src => src.TrainerPokemonWinnerId))
                .ForMember(dest => dest.LoserId, opt => opt.MapFrom(src => src.TrainerPokemonLoserId))
                .ForMember(dest => dest.GymId, opt => opt.MapFrom(src => src.Gym.Id))
                .ReverseMap();
            CreateMap<BattleTurn, BattleTurnDto>()
                .ForMember(dest => dest.AttackerId, opt => opt.MapFrom(src => src.AttackerId))
                .ForMember(dest => dest.DefenderId, opt => opt.MapFrom(src => src.DefenderId))
                .ForMember(dest => dest.BattleId, opt => opt.MapFrom(src => src.BattleId))
                .ReverseMap();
        }
    }
}
