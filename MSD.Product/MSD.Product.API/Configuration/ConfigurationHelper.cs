﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Repository.API;
using MSD.Product.Repository.Db.Context;
using Polly;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;

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
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        /// <summary>
        /// API Circuit Breaker
        /// </summary>
        public static IServiceCollection AddHttpClientWithRetryPolicies<TBase>(this IServiceCollection services)
        {
            var attempts = 2;
            var retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(
                    attempts,
                    onRetry: (message, retryCount) => Debug.WriteLine($"API Problem ({retryCount} attempt): {message}")
                );

            // -> namespace Microsoft.Extensions.DependencyInjection
            //      public static class HttpClientFactoryServiceCollectionExtensions
            //          public static IHttpClientBuilder AddHttpClient<TClient, [DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
            var httpClientFactoryMethod = typeof(HttpClientFactoryServiceCollectionExtensions).GetMethods().FirstOrDefault(e => e.GetGenericArguments().Count() == 2 && e.GetParameters().Count() == 1);

            ListTypesOf<TBase>().ForEach(type => {
                var httpFactoryResult = httpClientFactoryMethod.MakeGenericMethod(type.GetInterface($"I{type.Name}"), type).Invoke(null, new object[] { services });
                PollyHttpClientBuilderExtensions.AddPolicyHandler((IHttpClientBuilder)httpFactoryResult, retryPolicy);
            });

            return services;
        }
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
                var swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";

                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

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
