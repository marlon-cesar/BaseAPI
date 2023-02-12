using BaseAPI.Domain.DTO;
using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;

namespace  BaseAPI.Domain.Interfaces.Services
{
    public interface IPokemonService : ICrudServiceBase<Pokemon>
    {
        Task LoadPokemons();
        Task<PagedResult<PokemonDTO>> GetByName(int page, string expression = null);
    }
}
