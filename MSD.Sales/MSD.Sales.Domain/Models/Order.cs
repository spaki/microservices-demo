using MSD.Sales.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSD.Sales.Domain.Models
{
    public class Order : EntityBase
    {
        public Order()
        {
            Number = Guid.NewGuid().ToString("N");
            CreatedAtUtc = EditedAtUtc = DateTime.UtcNow;
        }

        public virtual string Number { get; set; }
        public virtual decimal TotalPurchaseAmout { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual DateTime EditedAtUtc { get; set; }

        public virtual List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public void AddItem(string externalId, string name, int quantity, decimal price)
        {
            var item = Items.FirstOrDefault(e => e.ExternalId == externalId);

            if (item == null)
            { 
                item = new OrderItem();
                Items.Add(item);
            }

            item.ExternalId = externalId;
            item.Name = name;
            item.Quantity += quantity;
            item.RowTotal += quantity * price;
        }
    }
}
