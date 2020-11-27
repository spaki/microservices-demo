using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSD.ZipCode.V1.API.Configuration;
using MSD.ZipCode.V1.API.Model;

namespace MSD.ZipCode.V1.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(AppSettings)
            .AddSwaggerGen()
            .AddControllers();

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
