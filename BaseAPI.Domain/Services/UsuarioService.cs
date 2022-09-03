using BaseAPI.Domain.Infra.Helpers;
using BaseAPI.Domain.Interfaces.Repositories;
using BaseAPI.Domain.Models;
using BaseAPI.Domain.Services.Common;
using  BaseAPI.Domain.Interfaces.Services;

namespace  BaseAPI.Domain.Services
{
    public class UsuarioService : CrudServiceBase<Usuario>, IUsuarioService
    {
        private new readonly IUsuarioRepository _repository;
        public UsuarioService(IUsuarioRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        public async Task<Usuario> AutenticarUsuario(string email, string senha) => 
            await this._repository.FirstOrDefaultAsync(u => u.Email.Equals(email) && !string.IsNullOrEmpty(senha) && u.Senha.Equals(senha.ToMD5()));

        public async Task<Usuario> GetUsuarioByEmail(string email) => 
            await this._repository.FirstOrDefaultAsync(u => u.Email.Equals(email));


    }
}
