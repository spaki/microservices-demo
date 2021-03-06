using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSD.Product.API.Configuration;
using MSD.Product.Domain.Services.Common;
using MSD.Product.Infra;
using MSD.Product.Infra.Warning;
using MSD.Product.Repository.API.Common;
using MSD.Product.Repository.Db.Common;
using MSD.Product.Repository.ZipCode.V1.API.Common;
using MSD.Product.Repository.ZipCode.V2.API.Common;

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
            .AddCustomSwaggerDocGenForApiVersioning()
            .AddScoped<WarningManagement>()
            .AddScopedByBaseType<ServiceBase>()
            .AddScopedByBaseType<RepositoryDbBase>()
            .AddHttpClientWithRetryPolicies<RepositoryApiBase>()
            .AddHttpClientWithRetryPolicies<ZipCodeRepositoryApiV1Base>()
            .AddHttpClientWithRetryPolicies<ZipCodeRepositoryApiV2Base>()
            .AddCustomControllers();

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
