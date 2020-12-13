using MSD.Sales.Domain.Interfaces.Repositories;
using MSD.Sales.Domain.Models;
using MSD.Sales.Repository.Db.Context;
using MSD.Sales.Repository.Db.Repositories.Common;

namespace MSD.Sales.Repository.Db.Repositories
{
    public class OrderRepositoryDb : CrudRepositoryBase<Order>, IOrderRepositoryDb
    {
        public OrderRepositoryDb(MainDbContext context) : base(context)
        {
        }
    }
}
