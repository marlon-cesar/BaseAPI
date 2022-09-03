using Microsoft.AspNetCore.Authorization;

namespace  BaseAPI.API.Auth
{
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        public BearerAuthorizeAttribute() : base("Bearer")
        {
        }

        public BearerAuthorizeAttribute(params string[] profiles) : base("Bearer") => this.Roles = string.Join(",", profiles);
    }
}
