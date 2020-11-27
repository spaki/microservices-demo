using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSD.ZipCode.V2.API.Configuration;
using MSD.ZipCode.V2.Domain.Infra.Settings;
using MSD.ZipCode.V2.Domain.Services.Common;
using MSD.ZipCode.V2.Repository.SOAP.Common;

namespace MSD.ZipCode.V2.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(AppSettings)
            .AddSwaggerGen()
            .AddScopedByBaseType<ServiceBase>()
            .AddScopedByBaseType<RepositorySoapBase>()
            .AddCustomControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app
            .UseCustomCors()
            .UseDeveloperExceptionDetailsPage(env)
            .UseHttpsRedirection()
            .UseCustomSwagger()
            .UseRouting()
            .UseAuthorization()
            .UseControllersEndpoints();
    }
}
