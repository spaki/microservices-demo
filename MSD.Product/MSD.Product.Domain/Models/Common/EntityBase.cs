using System;

namespace MSD.Product.Domain.Models.Common
{
    public abstract class EntityBase
    {
        public virtual Guid Id { get; set; }
    }
}
