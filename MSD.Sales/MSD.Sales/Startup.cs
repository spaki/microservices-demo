using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSD.Sales.Configuration;
using MSD.Sales.Domain.Handlers.Common;
using MSD.Sales.Infra.NotificationSystem;
using MSD.Sales.Infra.Settings;
using MSD.Sales.Repository.Db.Repositories.Common;
using MSD.Sales.Repository.Product.Api.Common;

namespace MSD.Sales
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(AppSettings)
            .AddCustomApiVersioning()
            .AddCustomSwaggerDocGenForApiVersioning()
            .AddCustomControllers()
            .AddSqlServerMainDb(AppSettings)
            .AddScoped<NotificationManagement>()
            .AddMediatR(typeof(HandlerBase))
            .AddScopedByBaseType<RepositoryDbBase>()
            .AddHttpClientWithRetryPolicies<ProductRepositoryApiBase>()
            ;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider) => app
            .UseCustomCors()
            .UseDeveloperExceptionDetailsPage(env)
            .UseHttpsRedirection()
            .UseCustomSwagger(provider)
            .UseRouting()
            .UseAuthorization()
            .UseControllersEndpoints()
            ;
    }
}
