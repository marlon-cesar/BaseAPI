using BaseAPI.Domain.Interfaces.Repositories;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;
using BaseAPI.Repository.Contexts;
using BaseAPI.Repository.Repositories.Common;

namespace BaseAPI.Repository.Repositories
{
    public class UsuarioRepository : CrudRepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MainDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<Usuario>> GetUsuarios(int pagina, int itensPorPagina) => await Page(Query(), pagina, itensPorPagina);
    }
}
