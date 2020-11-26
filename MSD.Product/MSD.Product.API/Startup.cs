using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSD.Product.API.Configuration;
using MSD.Product.Domain.Infra.Settings;
using MSD.Product.Domain.Services.Common;
using MSD.Product.Repository.API.Common;
using MSD.Product.Repository.Db.Common;

namespace MSD.Product.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(AppSettings)
            .AddMemoryDb()
            .AddCustomApiVersioning()
            .AddSwaggerGen()
            .AddScopedByBaseType<ServiceBase>()
            .AddScopedByBaseType<RepositoryDbBase>()
            .AddHttpClientWithRetryPolicies<RepositoryApiBase>()
            .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider) => app
            .UseCustomCors()
            .UseDeveloperExceptionDetailsPage(env)
            .UseHttpsRedirection()
            .UseCustomSwagger(provider)
            .UseRouting()
            .UseAuthorization()
            .UseControllersEndpoints();
    }
}
