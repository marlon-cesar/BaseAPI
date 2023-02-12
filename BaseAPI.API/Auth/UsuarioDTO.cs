using BaseAPI.Domain.Models;

namespace BaseAPI.API.Auth
{
    public class UsuarioDTO
    {
        public UsuarioDTO(Usuario usuario)
        {
            this.Id = usuario.Id.ToString();
            this.Nome = usuario.Nome;
            this.Email = usuario.Email;
            this.Perfil = usuario.TipoUsuario.ToString();
        }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }

    }
}
