using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models;

namespace  BaseAPI.Domain.Interfaces.Services
{
    public interface IUsuarioService : ICrudServiceBase<Usuario>
    {
        Task<Usuario> AutenticarUsuario(string email, string senha);

        Task<Usuario> GetUsuarioByEmail(string email);

    }
}
