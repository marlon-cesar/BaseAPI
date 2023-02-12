using BaseAPI.Domain.Interfaces.Repositories;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;
using BaseAPI.Repository.Contexts;
using BaseAPI.Repository.Repositories.Common;

namespace BaseAPI.Repository.Repositories
{
    public class PokemonTypeRepository : CrudRepositoryBase<PokemonType>, IPokemonTypeRepository
    {
        public PokemonTypeRepository(MainDbContext context) : base(context)
        {
        }

        public async Task<PokemonType> GetByName(string name) => await FirstOrDefaultAsync(t => t.Name.Equals(name));
    }
}
