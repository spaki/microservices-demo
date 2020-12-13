using System;
using System.Collections.Generic;
using System.Linq;

namespace MSD.Sales.Domain.Dtos
{
    public class Order
    {
        public Order(Models.Order entity)
        {
            Number = entity.Number;
            TotalPurchaseAmout = entity.TotalPurchaseAmout;
            CreatedAtUtc = entity.CreatedAtUtc;
            EditedAtUtc = entity.EditedAtUtc;

            Items = entity.Items.Select(e => new OrderItem(e)).ToList();
        }

        public string Number { get; set; }
        public decimal TotalPurchaseAmout { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime EditedAtUtc { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
