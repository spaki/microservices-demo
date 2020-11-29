using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Controllers.Common;
using MSD.Product.API.Models;
using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Services;
using MSD.Product.Infra.Warning;
using System.Threading.Tasks;
using System.Web;

namespace MSD.Product.API.Controllers.V1
{
    public class ZipCodeController : V1Controller
    {
        private readonly IZipCodeService zipCodeService;

        public ZipCodeController(
            WarningManagement warningManagement,
            IZipCodeService zipCodeService
        ) : base(warningManagement)
        {
            this.zipCodeService = zipCodeService;
        }

        [HttpGet("{zipCode}")]
        public async Task<ActionResult<ApiDefaultResponse<Address>>> GetByExternalIdAsync(string zipCode) => Response(await zipCodeService.GetAddressByZipCodeV1Async(HttpUtility.UrlDecode(zipCode)).ConfigureAwait(false));
    }
}
