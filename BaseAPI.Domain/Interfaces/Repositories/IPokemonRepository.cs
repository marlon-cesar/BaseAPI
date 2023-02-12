using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;

namespace BaseAPI.Domain.Interfaces.Repositories
{
    public interface IPokemonRepository : ICrudRepositoryBase<Pokemon>
    {
        Task<Pokemon> GetByName(string name);
        Task<Pokemon> GetByOrder(int order);
    }
}
