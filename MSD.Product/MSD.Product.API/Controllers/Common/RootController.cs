using Microsoft.AspNetCore.Mvc;
using MSD.Product.API.Models;
using MSD.Product.Infra.Warning;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MSD.Product.API.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class RootController : ControllerBase
    {
        private readonly WarningManagement warningManagement;

        public RootController(WarningManagement warningManagement)
        {
            this.warningManagement = warningManagement;
        }

        /// <summary>
        /// Check warnings and format the response
        /// </summary>
        /// <typeparam name="T">Payload Type</typeparam>
        /// <param name="result">Payload</param>
        /// <returns>Payload into formatted response</returns>
        protected ActionResult<ApiDefaultResponse<T>> Response<T>(T result)
        {
            var response = new ApiDefaultResponse<T>(result, !warningManagement.Any(), warningManagement.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Check warnings and format the response
        /// </summary>
        /// <param name="task">Task to handle with the request</param>
        /// <returns>Fromatted response</returns>
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
