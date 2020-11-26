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
            var result = new PagedResult<ProductListItemDto>(apiResult.Result.Page, apiResult.Result.TotalPages, apiResult.Result.Items.Select(e => new ProductListItemDto(e)));
            return result;
        }

        public void SetPrice(PriceDto dto)
        { 
            //var entity = 
        }
    }
}
