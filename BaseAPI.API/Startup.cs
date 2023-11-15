using BaseAPI.API.Auth;
using BaseAPI.API.Setup;
using BaseAPI.Domain.Infra.Settings;
using BaseAPI.Domain.Services.Common;
using BaseAPI.Repository.Repositories.Common;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.API
{
    public class Startup
    {
        public AppSettings AppSettings { get; }

        public Startup(IConfiguration configuration) => this.AppSettings = configuration.Get<AppSettings>();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(this.AppSettings)
                .AddSwagger()
                .AddJwtAuth(this.AppSettings)
                .AddPostgreSqlContexts(this.AppSettings)
                .AddHttpContextAcessor()
                .AddDistributedMemoryCache()
                .AddScopedByBaseType(typeof(ServiceBase))
                .AddScopedByBaseType(typeof(RepositoryBase))
                .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app
                .UseDeveloperExceptionPage()
                .UseCustomCors()
                .UseRouting()
                .UseAuthorization()
                .UseSawaggerWithDocs()
                .UseDatabaseInitialization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
