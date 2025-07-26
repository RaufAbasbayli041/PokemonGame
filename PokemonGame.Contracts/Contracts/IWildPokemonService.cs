using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;

namespace PokemonGame.Contracts.Contracts
{
	public interface IWildPokemonService : IGenericService<WildPokemon,WildPokemonDto>
	{
	}
}
