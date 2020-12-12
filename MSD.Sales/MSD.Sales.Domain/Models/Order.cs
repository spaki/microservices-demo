using MSD.Sales.Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace MSD.Sales.Domain.Models
{
    public class Order : EntityBase
    {
        public Order()
        {

        }

        public virtual string Number { get; set; }
        public virtual decimal TotalPurchaseAmout { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual DateTime EditedAtUtc { get; set; }

        public virtual List<OrderItem> Items { get; set; }
    }
}
