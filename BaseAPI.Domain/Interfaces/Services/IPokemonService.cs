using BaseAPI.Domain.DTO;
using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models;

namespace  BaseAPI.Domain.Interfaces.Services
{
    public interface IPokemonService : ICrudServiceBase<Pokemon>
    {
        Task LoadPokemons();
        Task<IEnumerable<PokemonDTO>> GetByName(string name);
    }
}
