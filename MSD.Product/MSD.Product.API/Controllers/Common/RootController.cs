using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Models;
using MSD.Product.Domain.Interfaces.Services;

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

        protected new IActionResult Response(object result = null)
        {
            var response = new ApiDefaultResponse(result, !warningService.Any(), warningService.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
