using MediatR;
using MSD.Sales.Domain.Commands;
using MSD.Sales.Domain.Dtos;
using MSD.Sales.Domain.Events;
using MSD.Sales.Domain.Handlers.Common;
using MSD.Sales.Domain.Interfaces.Repositories;
using MSD.Sales.Domain.Models;
using MSD.Sales.Infra.Api;
using MSD.Sales.Infra.NotificationSystem;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSD.Sales.Domain.Handlers
{
    public class OrderHandler : HandlerBase,
        IRequestHandler<OrderCreateCommand, OrderCreateResult>
    {
        private readonly IProductRepositoryApi productRepositoryApi;
        private readonly IOrderRepositoryDb orderRepositoryDb;

        public OrderHandler(
            NotificationManagement notificationManagement,
            IMediator mediator,
            IProductRepositoryApi productRepositoryApi,
            IOrderRepositoryDb orderRepositoryDb
        ) : base(notificationManagement, mediator)
        {
            this.productRepositoryApi = productRepositoryApi;
            this.orderRepositoryDb = orderRepositoryDb;
        }

        public async Task<OrderCreateResult> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = new Models.Order();

            foreach (var item in request.Items)
            {
                var productResponse = await productRepositoryApi.GetByExternalIdAsync(item.ExternalId);
                
                Validate(item, productResponse);

                if (notificationManagement.Any())
                    return null;

                entity.AddItem(item.ExternalId, productResponse.Payload.Name, item.Quantity, productResponse.Payload.Price.Value);
            }

            await orderRepositoryDb.SaveAsync(entity);
            await mediator.Publish(new OrderCreatedEvent { Number = entity.Number });

            var result = new OrderCreateResult { Number = entity.Number };
            return result;
        }

        private void Validate(OrderItemCreate item, ApiDefaultResponse<Product> productResponse)
        {
            if (item.Quantity < 1)
                notificationManagement.Add($"Product {item.ExternalId} has no valid quantity", NotificationType.Error);
            else if (!productResponse.Success)
                notificationManagement.Add(productResponse.Notifications);
            else if (productResponse.Payload == null)
                notificationManagement.Add($"Product {item.ExternalId} not found", NotificationType.NotFound);
            else if (productResponse.Payload.Price == null)
                notificationManagement.Add($"Product {item.ExternalId} has no set price", NotificationType.Error);
        }
    }
}
