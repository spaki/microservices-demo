using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Repository.Db.Common;
using MSD.Product.Repository.Db.Context;

namespace MSD.Product.Repository.Db
{
    public class ProductRepositoryDb : CrudRepositoryBase<Domain.Models.Product>, IProductRepositoryDb
    {
        public ProductRepositoryDb(EntitiesContext context) : base(context)
        {
        }
    }
}
