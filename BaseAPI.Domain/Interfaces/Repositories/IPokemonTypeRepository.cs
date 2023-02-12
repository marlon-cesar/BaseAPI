using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;

namespace BaseAPI.Domain.Interfaces.Repositories
{
    public interface IPokemonTypeRepository : ICrudRepositoryBase<PokemonType>
    {
        Task<PokemonType> GetByName(string name);
    }
}
