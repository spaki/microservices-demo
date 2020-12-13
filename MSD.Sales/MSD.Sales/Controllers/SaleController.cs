using MediatR;
using Microsoft.AspNetCore.Mvc;
using MSD.Sales.Controllers.Common;
using MSD.Sales.Domain.Commands;
using MSD.Sales.Domain.Dtos;
using MSD.Sales.Domain.Dtos.Common;
using MSD.Sales.Domain.Interfaces.Repositories;
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
        private readonly IOrderRepositoryDb orderRepositoryDb;

        public SaleController(
            NotificationManagement notificationManagement,
            IMediator mediator,
            IOrderRepositoryDb orderRepositoryDb
        ) : base(notificationManagement)
        {
            this.mediator = mediator;
            this.orderRepositoryDb = orderRepositoryDb;
        }

        [HttpGet]
        public async Task<ActionResult<ApiDefaultResponse<PagedResult<Order>>>> Get(int page = 1) => Response(orderRepositoryDb.ListPage(page));

        [HttpGet("{number}")]
        public async Task<ActionResult<ApiDefaultResponse<Order>>> Get(string number) => Response(await orderRepositoryDb.GetByNumberAsync(number).ConfigureAwait(false));

        [HttpPost]
        public async Task<ActionResult<ApiDefaultResponse<OrderCreateResult>>> Create(OrderCreateCommand command) => Response(await mediator.Send(command).ConfigureAwait(false));
    }
}
