using PokemonGame.Contracts.Dtos;
using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame.Contracts.Contracts
{
    public interface ICategoryService : IGenericService<Category,CategoryDto>
    {

    }
}
