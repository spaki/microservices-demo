using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Dtos.ProductDtos;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Domain.Interfaces.Services;
using MSD.Product.Domain.Services.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IWarningService warningService;
        private readonly IProductRepositoryApi productRepositoryApi;
        private readonly IProductRepositoryDb productRepositoryDb;

        public ProductService(
            IWarningService warningService,
            IProductRepositoryApi productRepositoryApi,
            IProductRepositoryDb productRepositoryDb
        )
        {
            this.warningService = warningService;
            this.productRepositoryApi = productRepositoryApi;
            this.productRepositoryDb = productRepositoryDb;
        }

        public async Task<PagedResult<ProductListItemDto>> SearchAsync(string value = null, int page = 1)
        {
            var apiResult = await productRepositoryApi.SearchAsync(value, page);

            if (apiResult.Success)
            {
                var result = new PagedResult<ProductListItemDto>(apiResult.Result.Page, apiResult.Result.TotalPages, apiResult.Result.Items.Select(e => new ProductListItemDto(e)));
                return result;
            }

            warningService.Add(apiResult.Warning);
            return null;
        }

        public async Task<ProductDto> GetByExternalIdAsync(string externalId)
        {
            var dbResult = await productRepositoryDb.FirstOrDefaultAsync(e => e.ExternalId == externalId);

            if (dbResult != null)
                return new ProductDto(dbResult);

            var apiResult = await productRepositoryApi.GetByExternalIdAsync(externalId);
            
            warningService.Add(apiResult.Warning);

            return apiResult.Result;
        }

        public async Task SetPriceAsync(PriceDto dto)
        {
            var entity = await productRepositoryDb.FirstOrDefaultAsync(e => e.ExternalId == dto.ProductExternalId);

            if (entity == null)
            { 
                var apiResult = await productRepositoryApi.GetByExternalIdAsync(dto.ProductExternalId);

                if (!apiResult.Success)
                {
                    warningService.Add(apiResult.Warning);
                    return;
                }

                entity = new Models.Product(apiResult.Result.Name, apiResult.Result.ExternalId, apiResult.Result.CreatedAtUtc);
            }

            warningService.Add(entity.SetPrice(dto.Price));

            if (!warningService.Any())
                await productRepositoryDb.SaveAsync(entity);
        }
    }
}
