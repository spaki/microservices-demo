using System;

namespace MSD.Product.Domain.Dtos.ProductDtos
{
    public class ProductDto
    {
        public ProductDto(Models.Product entity)
        {
            Id = entity.Id;
            Price = entity.Price;
            Name = entity.Name;
            ExternalId = entity.ExternalId;
            CreatedAtUtc = entity.CreatedAtUtc;
            EditedAtUtc = entity.EditedAtUtc;
        }

        public ProductDto(string name, string externalId, DateTime createdAtUtc)
        {
            Name = name;
            ExternalId = externalId;
            CreatedAtUtc = createdAtUtc;
        }

        public virtual string Name { get; set; }
        public virtual string ExternalId { get; set; }
        public virtual DateTime CreatedAtUtc { get; set; }

        public virtual Guid? Id { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual DateTime? EditedAtUtc { get; set; }
    }
}
