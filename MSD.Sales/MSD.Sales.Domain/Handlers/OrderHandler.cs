using MediatR;
using MSD.Sales.Domain.Commands;
using MSD.Sales.Domain.Dtos;
using MSD.Sales.Domain.Events;
using MSD.Sales.Domain.Handlers.Common;
using MSD.Sales.Infra.NotificationSystem;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSD.Sales.Domain.Handlers
{
    public class OrderHandler : HandlerBase,
        IRequestHandler<OrderCreateCommand, OrderCreateResult>
    {
        private readonly IMediator mediator;

        public OrderHandler(
            NotificationManagement notificationManagement,
            IMediator mediator
        ) : base(notificationManagement)
        {
            this.mediator = mediator;
        }

        public async Task<OrderCreateResult> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var result = new OrderCreateResult { Number = Guid.NewGuid().ToString() };
            await mediator.Publish(new OrderCreatedEvent { Number = result.Number });
            return result;
        }
    }
}
