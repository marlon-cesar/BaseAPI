using BaseAPI.API.Auth;
using BaseAPI.API.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.API.Controllers
{
    [ApiController]
    [BearerAuthorize("User, Admin")]
    [Route("api/[controller]")]
    public class PokedexController : AuthenticatedController
    {
        [HttpGet]
        public IActionResult Teste()
        {
            return Ok("Oi");
        }
    }
}