using MSD.Product.Domain.Models.Common;
using MSD.Product.Infra.Warning.Dtos;
using System;

namespace MSD.Product.Domain.Models
{
    public class Product : EntityBase
    {
        public Product()
        {

        }

        public Product(string name, string externalId, DateTime createdAtUtc)
        {
            Name = name;
            ExternalId = externalId;
            CreatedAtUtc = createdAtUtc;
        }

        public virtual decimal Price { get; set; }
        public virtual string Name { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }
        public virtual DateTime EditedAtUtc { get; set; }

        public WarningInfo SetPrice(decimal price) 
        {
            if (price > 0)
            {
                Price = price;
                EditedAtUtc = DateTime.UtcNow;
            }
            else 
            {
                return new WarningInfo("Invalid price!");
            }

            return null;
        }
    }
}
