using MediatR;
using Microsoft.AspNetCore.Mvc;
using MSD.Sales.Controllers.Common;
using MSD.Sales.Domain.Commands;
using MSD.Sales.Domain.Dtos;
using MSD.Sales.Infra.Api;
using MSD.Sales.Infra.NotificationSystem;
using System.Threading.Tasks;

namespace MSD.Sales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : RootController
    {
        private readonly IMediator mediator;

        public SaleController(
            NotificationManagement notificationManagement,
            IMediator mediator
        ) : base(notificationManagement)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public string Get() => "oi";

        [HttpPost]
        public async Task<ActionResult<ApiDefaultResponse<OrderCreateResult>>> Create(OrderCreateCommand command) => Response(await mediator.Send(command).ConfigureAwait(false));
    }
}
