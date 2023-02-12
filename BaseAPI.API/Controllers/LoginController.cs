using BaseAPI.API.Auth;
using BaseAPI.API.Controllers.Common;
using BaseAPI.Domain.Infra.Settings;
using BaseAPI.Domain.Interfaces.Services;
using BaseAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;

namespace BaseAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : AuthenticatedController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IDistributedCache _cache;
        private readonly AppSettings _appSettings;
        private readonly SigningConfigurations _signingConfigurations;

        public LoginController(IUsuarioService usuarioService,
            IDistributedCache cache,
            AppSettings appSettings,
            SigningConfigurations signingConfigurations)
        {
            this._usuarioService = usuarioService;
            this._cache = cache;
            this._appSettings = appSettings;
            this._signingConfigurations = signingConfigurations;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto authRequestDto)
        {
            var usuario = await this._usuarioService.AutenticarUsuario(authRequestDto.Email, authRequestDto.Senha);

            usuario ??= await GetUsuarioByRefreshToken(authRequestDto);

            if (usuario == null || !usuario.Ativo)
                return StatusCode((int)HttpStatusCode.Forbidden, new AuthResultDto { Authenticated = false, Message = "Usuário ou senha inválidos" });
            else
                return ResponseOK(AuthConfigHelper.GenerateToken(new UsuarioDTO(usuario), _appSettings, _cache, _signingConfigurations));

        }

        private async Task<Usuario?> GetUsuarioByRefreshToken(AuthRequestDto authRequestDto)
        {
            if (string.IsNullOrWhiteSpace(authRequestDto?.Email) || string.IsNullOrWhiteSpace(authRequestDto?.RefreshToken))
                return null;

            var storedToken = _cache.GetString(authRequestDto.RefreshToken);

            if (string.IsNullOrWhiteSpace(storedToken))
                return null;

            var refreshTokenDataStored = JsonConvert.DeserializeObject<RefreshTokenData>(storedToken);

            if (refreshTokenDataStored == null)
                return null;

            if (authRequestDto.Email == refreshTokenDataStored.Email && authRequestDto.RefreshToken == refreshTokenDataStored.RefreshToken)
                return await this._usuarioService.GetUsuarioByEmail(authRequestDto.Email);

            return null;
        }
    }
}
