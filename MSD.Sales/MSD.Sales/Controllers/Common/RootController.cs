using Microsoft.AspNetCore.Mvc;
using MSD.Sales.Infra.Api;
using MSD.Sales.Infra.NotificationSystem;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MSD.Sales.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class RootController : ControllerBase
    {
        private readonly NotificationManagement notificationManagement;

        public RootController(NotificationManagement notificationManagement)
        {
            this.notificationManagement = notificationManagement;
        }

        /// <summary>
        /// Check warnings and format the response
        /// </summary>
        /// <typeparam name="T">Payload Type</typeparam>
        /// <param name="result">Payload</param>
        /// <returns>Payload into formatted response</returns>
        protected ActionResult<ApiDefaultResponse<T>> Response<T>(T result)
        {
            var response = new ApiDefaultResponse<T>(result, !notificationManagement.Any(), notificationManagement.List());

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
            var response = new ApiDefaultResponseBase(!notificationManagement.Any(), notificationManagement.List());

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
