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
            ApiResult<T> result = null;

            var executionResult = await retryPolice.ExecuteAndCaptureAsync(async () =>
            {
                var response = await client.GetAsync(url);
                result = await GetObjectFromHttpResponseAsync<T>(response);

                // -> The execution should fail, if some problem happens during the request.
                response.EnsureSuccessStatusCode();

                return response;
            });

            if (result == null)
                result = new ApiResult<T>(url, new Exception("It was not possible to get the results"));

            result.SetException(executionResult.FinalException);

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

        private async Task<ApiResult<T>> GetObjectFromHttpResponseAsync<T>(HttpResponseMessage httpResponse)
        {
            var url = httpResponse.RequestMessage.RequestUri.AbsoluteUri;
            T data = default;

            try
            {
                // -> Trying to deserialize object without EnsureSuccessStatusCode()
                //      because it can have some valid data
                var json = await httpResponse.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<T>(json);
                return new ApiResult<T>(data, url);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error during the deserialization of http response content");
                return new ApiResult<T>(data, url, ex);
            }
        }
    }
}
