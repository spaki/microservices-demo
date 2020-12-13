using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MSD.Sales.Infra.NotificationSystem;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MSD.Sales.Infra.Api
{
    public abstract class ApiClient
    {
        string slash = "/";

        protected readonly HttpClient client;
        protected readonly ILogger<ApiClient> log;

        public ApiClient(
            HttpClient client,
            ILogger<ApiClient> log
        )
        {
            this.client = client;
            this.log = log;
        }

        public async Task<ApiDefaultResponse<T>> GetAsync<T>(string url)
        {
            // -> Circuit Break Pattern
            var retryPolice = GetRetryPolicy();
            ApiDefaultResponse<T> result = null;

            var executionResult = await retryPolice.ExecuteAndCaptureAsync(async () =>
            {
                var response = await client.GetAsync(url);
                result = await GetObjectFromHttpResponseAsync<T>(response);

                // -> The execution should fail, if some problem happens during the request.
                response.EnsureSuccessStatusCode();

                return response;
            });

            if (result == null)
                result = new ApiDefaultResponse<T>(default, false, new List<Notification> { new Notification(new Exception("It was not possible to get the results")), new Notification(executionResult.FinalException) });

            return result;
        }

        public async Task<ApiDefaultResponse<T>> GetAsync<T>(string url, params KeyValuePair<string, object>[] parameters) => await GetAsync<T>(BuildUrl(url, parameters));

        public string BuildUrl(string url, string endpoint, string urlParam, bool encodeUrlParam = false)
        {
            url = NormalizeSlashes(url);

            if (endpoint.StartsWith(slash))
                endpoint = endpoint.Substring(1);

            url = NormalizeSlashes(url + endpoint);

            if (encodeUrlParam)
                urlParam = HttpUtility.UrlEncode(urlParam);

            url += urlParam;

            return url;
        }

        public T JsonTo<T>(string json) => JsonConvert.DeserializeObject<T>(json);

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

        private async Task<ApiDefaultResponse<T>> GetObjectFromHttpResponseAsync<T>(HttpResponseMessage httpResponse)
        {
            T data = default;

            try
            {
                // -> Trying to deserialize object without EnsureSuccessStatusCode()
                //      because it can have some important data
                var json = await httpResponse.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<T>(json);
                return new ApiDefaultResponse<T>(data);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error during the deserialization of http response content");
                return new ApiDefaultResponse<T>(data, false, new List<Notification> { new Notification(ex) });
            }
        }

        private string NormalizeSlashes(string url)
        {
            if (!url.EndsWith(slash))
                url += slash;
            return url;
        }

        private AsyncRetryPolicy GetRetryPolicy() => Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                        Constants.CiscuitBreakerAttempts,
                        retryCount => TimeSpan.FromSeconds(Constants.CiscuitBreakerIntervalInSeconds),
                        (message, retryCount) => log.LogError($"API Problem ({retryCount} attempt): {message}")
                    );
    }
}
