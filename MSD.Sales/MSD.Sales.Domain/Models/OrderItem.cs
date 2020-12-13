using MSD.Sales.Domain.Models.Common;
using System;

namespace MSD.Sales.Domain.Models
{
    public class OrderItem : EntityBase
    {
        public OrderItem()
        {
            CreatedAtUtc = EditedAtUtc = DateTime.UtcNow;
        }

        public virtual string Name { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal RowTotal { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual DateTime EditedAtUtc { get; set; }

        public virtual Order Order { get; set; }
    }
}
