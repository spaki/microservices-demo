using Microsoft.AspNetCore.WebUtilities;
using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Infra.Settings;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSD.Product.Repository.API.Common
{
    public abstract class RepositoryApiBase : IRepositoryBase
    {
        private readonly HttpClient client;
        protected readonly AppSettings settings;

        public RepositoryApiBase(
            HttpClient client,
            AppSettings settings
        )
        {
            this.client = client;
            this.settings = settings;
        }

        public string BuildUrl(params KeyValuePair<string, object>[] parameters)
        {
            var url = settings.StarWarsApiUrl;

            if (parameters != null && parameters.Any())
            {
                var dictionary = parameters
                    .Where(e => e.Value != null && e.Key != null)
                    .ToDictionary(e => e.Key, e => e.Value.ToString());

                url = QueryHelpers.AddQueryString(url, dictionary);
            }

            return url;
        }

        public async Task<ApiResult<T>> Get<T>(params KeyValuePair<string, object>[] parameters)
        {
            var url = BuildUrl(parameters);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(json);
            var result = new ApiResult<T>(data, url, true);

            return result;
        }
    }
}
