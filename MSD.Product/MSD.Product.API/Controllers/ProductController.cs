using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Controllers.Common;
using MSD.Product.Domain.Dtos.ProductDtos;
using MSD.Product.Domain.Interfaces.Services;
using System.Threading.Tasks;
using System.Web;

namespace MSD.Product.API.Controllers
{
    public class ProductController : V1Controller
    {
        private readonly IProductService productService;

        public ProductController(
            IWarningService warningService,
            IProductService productService
        ) : base(warningService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(string value = null, int page = 1) => await Response(await productService.SearchAsync(value, page).ConfigureAwait(false));

        [HttpGet("{externalId}")]
        public async Task<IActionResult> GetByExternalIdAsync(string externalId) => await Response(await productService.GetByExternalIdAsync(HttpUtility.UrlDecode(externalId)).ConfigureAwait(false));

        [HttpPatch]
        public async Task<IActionResult> SearchAsync(PriceDto dto) => await Response(productService.SetPriceAsync(dto).ConfigureAwait(false));

        [HttpGet("price")]
        public async Task<IActionResult> SearchPricedAsync(string value = null, int page = 1) => await Response(await productService.SearchPricedAsync(value, page).ConfigureAwait(false));
    }
}
