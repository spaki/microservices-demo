using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Controllers.Common;
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
        public async Task<IActionResult> SearchAsync(string value = null, int page = 1) => Response(await productService.SearchAsync(value, page).ConfigureAwait(false));

        [HttpGet("{externalId}")]
        public async Task<IActionResult> GetByExternalIdAsync(string externalId) => Response(await productService.GetByExternalIdAsync(HttpUtility.UrlDecode(externalId)).ConfigureAwait(false));
    }
}
