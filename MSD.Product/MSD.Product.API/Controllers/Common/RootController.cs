using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Models;
using MSD.Product.Infra.Warning;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MSD.Product.API.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RootController : ControllerBase
    {
        private readonly WarningManagement warningManagement;

        public RootController(WarningManagement warningManagement)
        {
            this.warningManagement = warningManagement;
        }

        protected ActionResult<ApiDefaultResponse<T>> Response<T>(T result)
        {
            var response = new ApiDefaultResponse<T>(result, !warningManagement.Any(), warningManagement.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        protected async Task<ActionResult<ApiDefaultResponseBase>> Response(ConfiguredTaskAwaitable task)
        {
            await task;
            var response = new ApiDefaultResponseBase(!warningManagement.Any(), warningManagement.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
