using System;

namespace MSD.Product.Domain.Dtos.ProductDtos
{
    public class ProductListItemDto
    {
        public ProductListItemDto(Models.Product entity)
        {
            Name = entity.Name;
            ExternalId = entity.ExternalId;
            CreatedAtUtc = entity.CreatedAtUtc;
        }

        public string Name { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
