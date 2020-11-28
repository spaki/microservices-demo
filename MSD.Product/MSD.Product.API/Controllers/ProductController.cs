﻿using Microsoft.AspNetCore.Mvc;
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
    public class ProductController : V1Controller
    {
        private readonly IProductService productService;

        public ProductController(
            WarningManagement warningManagement,
            IProductService productService
        ) : base(warningManagement)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiDefaultResponse<PagedResult<ProductListItemDto>>>> SearchAsync(string value = null, int page = 1) => Response(await productService.SearchAsync(value, page).ConfigureAwait(false));

        [HttpGet("{externalId}")]
        public async Task<ActionResult<ApiDefaultResponse<ProductDto>>> GetByExternalIdAsync(string externalId) => Response(await productService.GetByExternalIdAsync(HttpUtility.UrlDecode(externalId)).ConfigureAwait(false));

        [HttpPatch]
        public async Task<ActionResult<ApiDefaultResponseBase>> SetPriceAsync(PriceDto dto) => await Response(productService.SetPriceAsync(dto).ConfigureAwait(false));

        [HttpGet("price")]
        public async Task<ActionResult<ApiDefaultResponse<PagedResult<ProductDto>>>> SearchPricedAsync(string value = null, int page = 1) => Response(await productService.SearchPricedAsync(value, page).ConfigureAwait(false));
    }
}
