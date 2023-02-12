using BaseAPI.API.Auth;
using System.Security.Claims;

namespace BaseAPI.API.Controllers.Common
{
    [BearerAuthorize]
    public abstract class AuthenticatedController : BaseController
    {
        public Guid UsuarioId { get { return Guid.Parse(this.GetClaim(ClaimTypes.NameIdentifier)); } }
        public string UserName { get { return this.GetClaim(ClaimTypes.Name); } }
        public string UserMail { get { return this.GetClaim(ClaimTypes.Email); } }
        public string UserProfile { get { return this.GetClaim(ClaimTypes.Role); } }
    }
}
