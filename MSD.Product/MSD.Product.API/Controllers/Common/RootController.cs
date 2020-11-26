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

        protected ActionResult<ApiDefaultResponse<T>> Response<T>(T result)
        {
            var response = new ApiDefaultResponse<T>(result, !warningService.Any(), warningService.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        protected async Task<ActionResult<ApiDefaultResponseBase>> Response(ConfiguredTaskAwaitable task)
        {
            await task;
            var response = new ApiDefaultResponseBase(!warningService.Any(), warningService.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
