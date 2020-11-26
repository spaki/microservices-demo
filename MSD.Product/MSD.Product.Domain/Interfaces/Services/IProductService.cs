using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Dtos.ProductDtos;
using MSD.Product.Domain.Interfaces.Services.Common;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Interfaces.Services
{
    public interface IProductService : IServiceBase
    {
        Task<PagedResult<ProductListItemDto>> SearchAsync(string value = null, int page = 1);
        Task<ProductDto> GetByExternalIdAsync(string externalId);
        Task SetPriceAsync(PriceDto dto);
        Task<PagedResult<ProductDto>> SearchPricedAsync(string value = null, int page = 1);
    }
}
