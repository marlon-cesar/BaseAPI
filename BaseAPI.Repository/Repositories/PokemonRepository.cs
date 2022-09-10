using BaseAPI.Domain.Interfaces.Repositories;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;
using BaseAPI.Repository.Contexts;
using BaseAPI.Repository.Repositories.Common;

namespace BaseAPI.Repository.Repositories
{
    public class PokemonRepository : CrudRepositoryBase<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(MainDbContext context) : base(context)
        {
        }

        public async Task<Pokemon> GetByName(string name) => await FirstOrDefaultAsync(t => t.Name.Equals(name));
        public async Task<Pokemon> GetByOrder(int order) => await FirstOrDefaultAsync(t => t.Order == order);
    }
}
