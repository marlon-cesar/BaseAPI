using BaseAPI.API.Auth;
using BaseAPI.API.Controllers.Common;
using BaseAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.API.Controllers
{
    [ApiController]
    //[BearerAuthorize("User, Admin")]
    [Route("api/[controller]")]
    public class PokedexController : AuthenticatedController
    {
        private readonly IPokemonService _pokedexService;

        public PokedexController(IPokemonService pokedexService)
        {
            _pokedexService = pokedexService;
        }


        [AllowAnonymous]
        [HttpPatch("Load")]
        public async Task<IActionResult> LoadPokemons()
        {
            await _pokedexService.LoadPokemons();
            return Ok("Pokémons loaded to database");
        }

        [AllowAnonymous]
        [HttpGet("{page}")]
        public async Task<IActionResult> GetPokemonByName(int page, string expression = null) =>
             Ok(await _pokedexService.GetByName(page, expression));
    }
}