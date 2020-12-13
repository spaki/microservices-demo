using System;

namespace MSD.Sales.Domain.Dtos
{
    public class OrderItem
    {
        public OrderItem(Models.OrderItem entity)
        {
            Name = entity.Name;
            ExternalId = entity.ExternalId;
            Quantity = entity.Quantity;
            RowTotal = entity.RowTotal;
            CreatedAtUtc = entity.CreatedAtUtc;
            EditedAtUtc = entity.EditedAtUtc;
        }

        public string Name { get; set; }
        public string ExternalId { get; set; }
        public int Quantity { get; set; }
        public decimal RowTotal { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime EditedAtUtc { get; set; }
    }
}
