using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Models.Common;

namespace BaseAPI.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : ICrudRepositoryBase<Usuario>
    {
        Task<PagedResult<Usuario>> GetUsuarios(int pagina, int itensPorPagina);
    }
}
