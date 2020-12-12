using MSD.Sales.Infra.NotificationSystem;

namespace MSD.Sales.Domain.Handlers.Common
{
    public abstract class HandlerBase 
    {
        private readonly NotificationManagement notificationManagement;

        public HandlerBase(NotificationManagement notificationManagement)
        {
            this.notificationManagement = notificationManagement;
        }
    }
}
