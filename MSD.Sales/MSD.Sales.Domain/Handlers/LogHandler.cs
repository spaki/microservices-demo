using MediatR;
using Microsoft.Extensions.Logging;
using MSD.Sales.Domain.Events;
using MSD.Sales.Domain.Handlers.Common;
using MSD.Sales.Infra.NotificationSystem;
using System.Threading;
using System.Threading.Tasks;

namespace MSD.Sales.Domain.Handlers
{
    public class LogHandler : HandlerBase,
        INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger<LogHandler> log;

        public LogHandler(
            NotificationManagement notificationManagement,
            ILogger<LogHandler> log
        ) : base(notificationManagement)
        {
            this.log = log;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            // -> the idea here is, in the future, send an mail, for example.
            return Task.Run(() => log.LogInformation($"Order {notification.Number} created"));
        }
    }
}
