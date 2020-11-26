using Microsoft.AspNetCore.WebUtilities;
using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Infra;
using MSD.Product.Domain.Infra.Settings;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Repository.API.Common;
using MSD.Product.Repository.API.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSD.Product.Repository.API
{
    public class ProductRepositoryApi : RepositoryApiBase, IProductRepositoryApi
    {
        private readonly HttpClient client;

        public ProductRepositoryApi(
            HttpClient client, 
            AppSettings settings
        ) : base(settings)
        {
            this.client = client;
        }

        public async Task<ApiResult<PagedResult<Domain.Models.Product>>> SearchAsync(string value = null, int page = 1)
        {
            var url = BuildUrl(new KeyValuePair<string, object>("search", value), new KeyValuePair<string, object>("page", page.ToString()));

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<PageDto<PeopleDto>>(json);

            var totalPages = dto.count / Constants.DefaultPageSize;
            var result = new ApiResult<PagedResult<Domain.Models.Product>>(ConvertApiDtoToDomain(page, dto, totalPages), url, true);

            return result;
        }

        private static PagedResult<Domain.Models.Product> ConvertApiDtoToDomain(int page, PageDto<PeopleDto> dto, int totalPages) => new PagedResult<Domain.Models.Product>(page, totalPages, dto.results.Select(e => new Domain.Models.Product(e.name, e.url, e.created)).ToList());
    }
}
