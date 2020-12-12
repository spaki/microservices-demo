using MediatR;
using MSD.Sales.Domain.Dtos;
using System.Collections.Generic;

namespace MSD.Sales.Domain.Commands
{
    public class OrderCreateCommand : IRequest<OrderCreateResult>
    {
        public List<OrderItemCreate> Items { get; set; }
    }
}
