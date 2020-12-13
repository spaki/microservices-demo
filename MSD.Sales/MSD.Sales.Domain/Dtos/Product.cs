using System;

namespace MSD.Sales.Domain.Dtos
{
    public class Product
    {
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public Guid? Id { get; set; }
        public decimal? Price { get; set; }
        public DateTime? EditedAtUtc { get; set; }
    }
}
