using MediatR;
using MSD.Sales.Infra.NotificationSystem;

namespace MSD.Sales.Domain.Handlers.Common
{
    public abstract class HandlerBase 
    {
        protected readonly NotificationManagement notificationManagement;
        protected readonly IMediator mediator;


        public HandlerBase(
            NotificationManagement notificationManagement,
            IMediator mediator
        )
        {
            this.notificationManagement = notificationManagement;
            this.mediator = mediator;
        }
    }
}
