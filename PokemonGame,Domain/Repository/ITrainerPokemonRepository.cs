using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Repository
{
    public interface ITrainerPokemonRepository : IGenericRepository<TrainerPokemon>
    {
        //public Task<Trainer> GetTrainerByIdAsync(int trainerId);
        //public Task<Pokemon> GetPokemonByIdAsync(int pokemonId);
        Task TransferPokemon (int pokemonId,int newTrainerId);
        Task CaughtPokemon (int pokemonId, int trainerId);

    }
}
