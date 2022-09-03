using BaseAPI.API.Auth;
using BaseAPI.API.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.API.Controllers
{
    [ApiController]
    [BearerAuthorize("Candidato")]
    [Route("api/[controller]")]
    public class PokedexController : AuthenticatedController
    {
        
    }
}