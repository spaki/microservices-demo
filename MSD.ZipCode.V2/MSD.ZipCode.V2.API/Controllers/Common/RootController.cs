using Microsoft.AspNetCore.Mvc;
using MSD.ZipCode.V2.API.Models;
using MSD.ZipCode.V2.Domain.Interfaces.Services;

namespace MSD.ZipCode.V2.API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
