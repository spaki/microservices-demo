using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Controllers.Common;
using MSD.Product.API.Models;
using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Dtos.ProductDtos;
using MSD.Product.Domain.Interfaces.Services;
using MSD.Product.Infra.Warning;
using System.Threading.Tasks;
using System.Web;

namespace MSD.Product.API.Controllers
{
    [ApiVersion("1", Deprecated = true)]
    [ApiVersion("2")]
    public class ProductController : RootController
    {
        private readonly IProductService productService;

        public ProductController(
            WarningManagement warningManagement,
            IProductService productService
        ) : base(warningManagement)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Search a Star Wars "product", in the "https://swapi.dev/api/people/" API
        /// </summary>
        /// <param name="value">search value</param>
        /// <param name="page">page (for paged result)</param>
        /// <returns>API Default Pattern Result of a Paged Result for Products Items</returns>
        [HttpGet]
        public async Task<ActionResult<ApiDefaultResponse<PagedResult<ProductListItemDto>>>> SearchAsync(string value = null, int page = 1) => Response(await productService.SearchAsync(value, page).ConfigureAwait(false));

        /// <summary>
        /// Get one specific item from "https://swapi.dev/api/people/" API
        /// </summary>
        /// <param name="externalId">Url with teh Id of the Item</param>
        /// <returns>API Default Pattern Result of a Product DTO</returns>
        [HttpGet("{externalId}")]
        public async Task<ActionResult<ApiDefaultResponse<ProductDto>>> GetByExternalIdAsync(string externalId) => Response(await productService.GetByExternalIdAsync(HttpUtility.UrlDecode(externalId)).ConfigureAwait(false));

        /// <summary>
        /// Set a price for an item from "https://swapi.dev/api/people/" API, sand persists it in a memory db
        /// </summary>
        /// <param name="dto">Item Id and decimal price</param>
        /// <returns>API Default Pattern Result</returns>
        [HttpPatch]
        public async Task<ActionResult<ApiDefaultResponseBase>> SetPriceAsync(PriceDto dto) => await Response(productService.SetPriceAsync(dto).ConfigureAwait(false));

        /// <summary>
        /// Search a Star Wars "product", with price set in the memory db
        /// </summary>
        /// <param name="value">search value</param>
        /// <param name="page">page (for paged result)</param>
        /// <returns>API Default Pattern Result of a Paged Result for Product DTO</returns>
        [HttpGet("price")]
        public async Task<ActionResult<ApiDefaultResponse<PagedResult<ProductDto>>>> SearchPricedAsync(string value = null, int page = 1) => Response(await productService.SearchPricedAsync(value, page).ConfigureAwait(false));
    }
}
