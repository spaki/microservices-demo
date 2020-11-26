using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Infra;
using MSD.Product.Domain.Infra.Settings;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Repository.API.Common;
using MSD.Product.Repository.API.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSD.Product.Repository.API
{
    public class ProductRepositoryApi : RepositoryApiBase, IProductRepositoryApi
    {
        public ProductRepositoryApi(
            HttpClient client, 
            AppSettings settings
        ) : base(client, settings)
        { }

        public async Task<ApiResult<PagedResult<Domain.Models.Product>>> SearchAsync(string value = null, int page = 1)
        {
            var dto = await Get<PageDto<PeopleDto>>(new KeyValuePair<string, object>("search", value), new KeyValuePair<string, object>("page", page.ToString()));

            var totalPages = dto.Result.count / Constants.DefaultPageSize;
            var result = dto.To(e => ConvertApiDtoToDomain(page, e, totalPages));

            return result;
        }

        private static PagedResult<Domain.Models.Product> ConvertApiDtoToDomain(int page, PageDto<PeopleDto> dto, int totalPages) => new PagedResult<Domain.Models.Product>(page, totalPages, dto.results.Select(e => new Domain.Models.Product(e.name, e.url, e.created)).ToList());
    }
}
