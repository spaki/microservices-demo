using MediatR;

namespace MSD.Sales.Domain.Events
{
    public class OrderCreatedEvent : INotification
    {
        public string Number { get; set; }
    }
}
