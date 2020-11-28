using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MSD.ZipCode.V2.Domain.Infra;
using MSD.ZipCode.V2.Domain.Interfaces.Repositories.Common;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Threading.Tasks;

namespace MSD.ZipCode.V2.Repository.SOAP.Common
{
    public abstract class RepositorySoapBase : IRepositoryBase
    {
        protected readonly ILogger<RepositorySoapBase> log;
        private readonly IDistributedCache cache;

        public RepositorySoapBase(
            IDistributedCache cache,
            ILogger<RepositorySoapBase> log
        )
        {
            this.log = log;
            this.cache = cache;
        }

        protected async Task<T> GetFromCacheAndSetAsync<T>(string area, string key, Func<Task<T>> getAsync)
        {
            var fullKey = $"{area}-{key}";
            var json = await cache.GetStringAsync(fullKey);

            if (!string.IsNullOrWhiteSpace(json))
            {
                log.LogInformation($"{fullKey} retrieved from cache");
                return JsonConvert.DeserializeObject<T>(json);
            }

            var result = await getAsync();
            json = JsonConvert.SerializeObject(result);

            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(Constants.CacheTimeoutInSeconds));

            await cache.SetStringAsync(fullKey, json, cacheOptions);

            log.LogInformation($"{fullKey} retrieved from WS");

            return result;
        }

        protected AsyncRetryPolicy GetRetryPolicy() => Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                        Constants.CiscuitBreakerAttempts,
                        retryCount => TimeSpan.FromSeconds(Constants.CiscuitBreakerIntervalInSeconds),
                        (message, retryCount) => log.LogError($"SOAP Problem ({retryCount} attempt): {message}")
                    );
    }
}
