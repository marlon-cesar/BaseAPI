using BaseAPI.Domain.Infra.Helpers;
using BaseAPI.Domain.Infra.Settings;
using BaseAPI.Repository.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Reflection;

namespace BaseAPI.API.Setup
{
    public static class SetupHelper
    {
        #region Swagger
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Base API",
                    Description = "Pokédex API",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSawaggerWithDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Base API");
            });

            return app;
        }

        #endregion

        #region Database
        public static IServiceCollection AddSqlServerContexts(this IServiceCollection services, AppSettings settings)
        {
            var connectionString = settings.ConnectionStrings.MainDbConnection;

            services.AddDbContext<MainDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
                );

            return services;
        }

        public static IApplicationBuilder UseDatabaseInitialization(this IApplicationBuilder app)
        {
            var scopes = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

            if (scopes != null)
            {
                using (var scope = scopes)
                {
                    scope.ServiceProvider.GetRequiredService<MainDbContext>().Database.Migrate();
                }
            }

            return app;
        }

        #endregion



        public static IServiceCollection AddHttpContextAcessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            return services;
        }


        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            return app;
        }

        public static IServiceCollection AddScopedByBaseType(this IServiceCollection services, Type baseType)
        {
            var assemblies = Assembly
                .GetAssembly(baseType)
                .GetTypesOf(baseType);

            foreach (var type in assemblies)
            {
                services.AddScoped(type.GetInterface($"I{type.Name}"), type);
            }

            return services;
        }

        public static IServiceCollection AddScopedHandlers(this IServiceCollection services, Type handlerType, Assembly assembly)
        {
            assembly
                .GetTypes()
                .ToList()
                .ForEach(type =>
                    type
                        .GetInterfaces()
                        .Where(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == handlerType)
                        .ToList()
                        .ForEach(@interface => services.AddScoped(@interface, type))
                );

            return services;
        }

    }
}
