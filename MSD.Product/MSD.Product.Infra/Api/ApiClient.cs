using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MSD.Product.Infra.Api.Dtos;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MSD.Product.Infra.Api
{
    public abstract class ApiClient
    {
        private readonly HttpClient client;
        protected readonly ILogger<ApiClient> log;

        public ApiClient(
            HttpClient client,
            ILogger<ApiClient> log
        )
        {
            this.client = client;
            this.log = log;
        }

        public async Task<ApiResult<T>> GetAsync<T>(string url)
        {
            var retryPolice = GetRetryPolicy();
            string json = null;
            T data = default;

            var executionResult = await retryPolice.ExecuteAndCaptureAsync(async () =>
            {
                var response = await client.GetAsync(url);

                try
                {
                    json = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<T>(json);
                }
                catch
                { }

                response.EnsureSuccessStatusCode();

                return response;
            });

            if (executionResult.FinalException != null)
                return new ApiResult<T>(data, url, executionResult.FinalException);

            var result = new ApiResult<T>(data, url);

            return result;
        }
        //public async Task<ApiResult<T>> GetAsync<T>(string url)
        //{
        //    var retryPolice = GetRetryPolicy();

        //    var executionResult = await retryPolice.ExecuteAndCaptureAsync(async () =>
        //    {
        //        var response = await client.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        return response;
        //    });

        //    if (executionResult.FinalException != null)
        //        return new ApiResult<T>(url, executionResult.FinalException);

        //    var json = await executionResult.Result.Content.ReadAsStringAsync();
        //    var data = JsonConvert.DeserializeObject<T>(json);
        //    var result = new ApiResult<T>(data, url);

        //    return result;
        //}

        public async Task<ApiResult<T>> GetAsync<T>(string url, params KeyValuePair<string, object>[] parameters) => await GetAsync<T>(BuildUrl(url, parameters));

        public string BuildUrl(string url, string endpoint, bool encodeEndpoint = false)
        {
            var slash = "/";

            if (!url.EndsWith(slash))
                url += slash;

            if (encodeEndpoint)
                endpoint = HttpUtility.UrlEncode(endpoint);

            if (endpoint.StartsWith(slash))
                endpoint = endpoint.Substring(1);

            url = url + endpoint;

            return url;
        }

        private string BuildUrl(string url, params KeyValuePair<string, object>[] parameters)
        {
            if (parameters != null && parameters.Any())
            {
                var dictionary = parameters
                    .Where(e => e.Value != null && e.Key != null)
                    .ToDictionary(e => e.Key, e => e.Value.ToString());

                url = QueryHelpers.AddQueryString(url, dictionary);
            }

            return url;
        }

        private AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy() => Policy
                    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .WaitAndRetryAsync(
                        Constants.CiscuitBreakerAttempts,
                        retryCount => TimeSpan.FromSeconds(Constants.CiscuitBreakerIntervalInSeconds),
                        (message, retryCount) => log.LogError($"API Problem ({retryCount} attempt): {message}")
                    );
    }
}
