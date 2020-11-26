using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Dtos.ProductDtos;
using MSD.Product.Domain.Infra;
using MSD.Product.Domain.Infra.Settings;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Repository.API.Common;
using MSD.Product.Repository.API.Dtos;
using System;
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
            AppSettings settings,
            ILogger<ProductRepositoryApi> log
        ) : base(client, settings, log)
        { }

        public async Task<ApiResult<PagedResult<Domain.Models.Product>>> SearchAsync(string value = null, int page = 1)
        {
            var dto = await GetAsync<PageDto<PeopleDto>>(new KeyValuePair<string, object>("search", value), new KeyValuePair<string, object>("page", page.ToString()));
            var totalPages = (int)Math.Ceiling(dto.Result?.count ?? 0 / Constants.DefaultPageSize * 1m);
            var result = dto.To(e => ConvertApiDtoToDomain(page, e, totalPages));
                
            return result;
        }

        public async Task<ApiResult<ProductDto>> GetByExternalIdAsync(string externalId)
        {
            var dto = await GetAsync<PeopleDto>(externalId);
            var result = dto.To(e => ConvertApiDtoToDomain(e));
            return result;
        }
        
        private static PagedResult<Domain.Models.Product> ConvertApiDtoToDomain(int page, PageDto<PeopleDto> dto, int totalPages) => new PagedResult<Domain.Models.Product>(page, totalPages, dto.results.Select(e => new Domain.Models.Product(e.name, e.url, e.created)).ToList());
        private static ProductDto ConvertApiDtoToDomain(PeopleDto dto) => new ProductDto(dto.name, dto.url, dto.created);
    }
}
