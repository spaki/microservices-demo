using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Models;
using MSD.Product.Domain.Interfaces.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MSD.Product.API.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RootController : ControllerBase
    {
        private readonly IWarningService warningService;

        public RootController(IWarningService warningService)
        {
            this.warningService = warningService;
        }

        protected async Task<IActionResult> Response(object result = null)
        {
            var payload = result;
            if (result is ConfiguredTaskAwaitable)
            {
                await (ConfiguredTaskAwaitable)result;
                payload = null;
            }

            var response = new ApiDefaultResponse(payload, !warningService.Any(), warningService.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
