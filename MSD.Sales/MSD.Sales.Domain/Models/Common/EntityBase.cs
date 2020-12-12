using System;

namespace MSD.Sales.Domain.Models.Common
{
    public abstract class EntityBase
    {
        public virtual Guid Id { get; set; }
    }
}
