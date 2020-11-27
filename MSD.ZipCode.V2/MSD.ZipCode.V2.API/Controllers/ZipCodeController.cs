using Microsoft.AspNetCore.Mvc;
using MSD.ZipCode.V2.API.Controllers.Common;
using MSD.ZipCode.V2.API.Models;
using MSD.ZipCode.V2.Domain.Dtos;
using MSD.ZipCode.V2.Domain.Interfaces.Services;
using System.Threading.Tasks;
using System.Web;

namespace MSD.ZipCode.V2.API.Controllers
{
    public class ZipCodeController : RootController
    {
        private readonly IZipCodeService zipCodeService;

        public ZipCodeController(
            IWarningService warningService,
            IZipCodeService zipCodeService
        ) : base(warningService)
        {
            this.zipCodeService = zipCodeService;
        }

        [HttpGet("{zipCode}")]
        public async Task<ActionResult<ApiDefaultResponse<Address>>> GetAsync(string zipCode) => Response(await zipCodeService.GetAddressAsync(HttpUtility.UrlDecode(zipCode)).ConfigureAwait(false));
    }
}
