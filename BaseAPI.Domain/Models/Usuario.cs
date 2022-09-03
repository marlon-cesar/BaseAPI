using BaseAPI.Domain.Enums;
using BaseAPI.Domain.Models.Common;

namespace BaseAPI.Domain.Models
{
    public class Usuario : EntityBase
    {
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? PrimeiroNome { get => $"{Nome?.Split(' ')[0]}"; }
        public string? Senha { get; set; }
        public bool Ativo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

    }
}
