using PokemonGame_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGame_Domain.Repository
{
    public interface IPokemonRepository : IGenericRepository<Pokemon>
    {
        Task<Pokemon> UploadImgAsyn (int id,string imagePath);
    }
    
}
