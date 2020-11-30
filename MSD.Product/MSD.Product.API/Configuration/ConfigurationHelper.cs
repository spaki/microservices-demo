using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MSD.Product.Repository.Db.Context;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace MSD.Product.API.Configuration
{
    public static class ConfigurationHelper
    {
        public static IServiceCollection AddScopedByBaseType<TBase>(this IServiceCollection services)
        {
            ListTypesOf<TBase>().ForEach(type => services.AddScoped(type.GetInterface($"I{type.Name}"), type));

            return services;
        }



        public static IServiceCollection AddMemoryDb(this IServiceCollection services)
        {
            services.AddDbContext<EntitiesContext>(options => options.UseInMemoryDatabase("Entities"));

            return services;
        }

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(p =>
            {
                //p.DefaultApiVersion = new ApiVersion(1, 0);
                p.DefaultApiVersion = new ApiVersion(2, 0);
                p.ReportApiVersions = true; // -> api version info in the header 
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IServiceCollection AddCustomSwaggerDocGenForApiVersioning(this IServiceCollection services)
        {
            // -> "compile" the IoC container to get the api version list from provider.
            var provider = services.BuildServiceProvider();
            var apiVersionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options => {
                foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    options.SwaggerDoc(item.GroupName, new OpenApiInfo { Title = "Micro Service Demo - Product API", Version = item.ApiVersion.ToString() });

                // -> Get the comments from controllers actions to swagger use them
                options.IncludeXmlComments(
                    Path.Combine(
                        AppContext.BaseDirectory, 
                        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")
                    );
            });

            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            // -> format enums as strings
            services
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            return services;
        }

        // -> Ok, we have a bit of a mess.
        //      We need to add the http clients to IoC container manage the http sockets (it is a limited OS ressources).
        //      First, I need a way to register the API (http) clients, dinamically, type based.
        //      I have the possibility to register the Polly Circuit Breaker Policy, for those clients, 
        //      but it would be a poor situation to handle with the fail attempts.
        public static IServiceCollection AddHttpClientWithRetryPolicies<TBase>(this IServiceCollection services)
        {
            //var attempts = 3;
            //var interval = TimeSpan.FromSeconds(2);

            //var retryPolicy = Policy
            //    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            //    .WaitAndRetryAsync(
            //        attempts,
            //        retryCount => interval,
            //        (message, retryCount) => Debug.WriteLine($"API Problem ({retryCount} attempt): {message}")
            //    );

            // -> namespace Microsoft.Extensions.DependencyInjection
            //      public static class HttpClientFactoryServiceCollectionExtensions
            //          public static IHttpClientBuilder AddHttpClient<TClient, [DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
            var httpClientFactoryMethod = typeof(HttpClientFactoryServiceCollectionExtensions).GetMethods().FirstOrDefault(e => e.GetGenericArguments().Count() == 2 && e.GetParameters().Count() == 1);

            ListTypesOf<TBase>().ForEach(type =>
            {
                var httpFactoryResult = httpClientFactoryMethod.MakeGenericMethod(type.GetInterface($"I{type.Name}"), type).Invoke(null, new object[] { services });
                //PollyHttpClientBuilderExtensions.AddPolicyHandler((IHttpClientBuilder)httpFactoryResult, retryPolicy);
            });

            return services;
        }
        //public static IServiceCollection AddHttpClientWithRetryPolicies<TBase>(this IServiceCollection services)
        //{
        //    var attempts = 3;
        //    var interval = TimeSpan.FromSeconds(2);

        //    var retryPolicy = Policy
        //        .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        //        .WaitAndRetryAsync(
        //            attempts,
        //            retryCount => interval,
        //            (message, retryCount) => Debug.WriteLine($"API Problem ({retryCount} attempt): {message}")
        //        );

        //    // -> namespace Microsoft.Extensions.DependencyInjection
        //    //      public static class HttpClientFactoryServiceCollectionExtensions
        //    //          public static IHttpClientBuilder AddHttpClient<TClient, [DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
        //    var httpClientFactoryMethod = typeof(HttpClientFactoryServiceCollectionExtensions).GetMethods().FirstOrDefault(e => e.GetGenericArguments().Count() == 2 && e.GetParameters().Count() == 1);

        //    ListTypesOf<TBase>().ForEach(type => {
        //        var httpFactoryResult = httpClientFactoryMethod.MakeGenericMethod(type.GetInterface($"I{type.Name}"), type).Invoke(null, new object[] { services });
        //        PollyHttpClientBuilderExtensions.AddPolicyHandler((IHttpClientBuilder)httpFactoryResult, retryPolicy);
        //    });

        //    return services;
        //}
        //public static IServiceCollection AddHttpClientWithRetryPolicies(this IServiceCollection services)
        //{
        //    var attempts = 2;
        //    var retryPolicy = Policy
        //        .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        //        .RetryAsync(
        //            attempts,
        //            onRetry: (message, retryCount) => Debug.WriteLine($"API Problem ({retryCount} attempt): {message}")
        //        );

        //    services.AddHttpClient<IProductRepositoryApi, ProductRepositoryApi>().AddPolicyHandler(retryPolicy);

        //    return services;
        //}



        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            return app;
        }

        public static IApplicationBuilder UseDeveloperExceptionDetailsPage(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            return app;
        }

        public static IApplicationBuilder UseControllersEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                // -> helps to handle with the Areas, case exists
                var swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";

                var versions = provider.ApiVersionDescriptions.ToList();
                versions.Reverse();

                // -> drop down list for the doc spec
                foreach (var description in versions)
                    options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{description.GroupName}/swagger.json", description.GroupName);

                options.DocExpansion(DocExpansion.List);
            });

            return app;
        }



        private static List<Type> ListTypesOf<TBase>()
        {
            var baseType = typeof(TBase);

            var result = Assembly
                .GetAssembly(baseType)
                .GetTypes()
                .Where(type =>
                    type.BaseType != null
                    && (
                        (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == baseType)) // -> Generics, ex: CrudRepository<>
                        || (baseType.IsAssignableFrom(type) && !type.IsAbstract) // -> Non generics, ex: Repository
                    )
                .ToList();

            return result;
        }
    }
}
