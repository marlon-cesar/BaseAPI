using BaseAPI.Domain.Infra.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace BaseAPI.API.Auth
{
    public static class AuthConfigHelper
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, AppSettings appSettings)
        {
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services
                .AddAuthentication
                (
                    authOptions =>
                    {
                        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                ).AddJwtBearer
                (
                    bearerOptions =>
                    {
                        var paramsValidation = bearerOptions.TokenValidationParameters;
                        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                        paramsValidation.ValidAudience = appSettings.TokenSettings.Audience;
                        paramsValidation.ValidIssuer = appSettings.TokenSettings.Issuer;

                        paramsValidation.ValidateIssuerSigningKey = true;
                        paramsValidation.ValidateLifetime = true;

                        paramsValidation.ClockSkew = TimeSpan.Zero;
                    }
                );

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​).RequireAuthenticatedUser().Build());
            });

            return services;
        }

        public static AuthResultDto GenerateToken(UsuarioDTO user, AppSettings appSettings, IDistributedCache cache, SigningConfigurations signingConfigurations)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Email, "Login"),
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Perfil),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                }
            );

            var created = DateTime.Now;
            var expiration = created + TimeSpan.FromSeconds(appSettings.TokenSettings.Timeout);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = appSettings.TokenSettings.Issuer,
                Audience = appSettings.TokenSettings.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = created,
                Expires = expiration
            });

            var token = handler.WriteToken(securityToken);

            var result = new AuthResultDto
            {
                Authenticated = true,
                Created = created.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expiration.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                RefreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty),
                Message = "OK",
            };

            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(appSettings.TokenSettings.FinalExpiration));

            cache.SetString
            (
                result.RefreshToken,
                JsonConvert.SerializeObject(new RefreshTokenData { RefreshToken = result.RefreshToken, Email = user.Email }),
                cacheOptions
            );

            return result;
        }
    }
}
