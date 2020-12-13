using MSD.Sales.Domain.Dtos.Common;
using MSD.Sales.Domain.Interfaces.Repositories;
using MSD.Sales.Domain.Models;
using MSD.Sales.Repository.Db.Context;
using MSD.Sales.Repository.Db.Repositories.Common;
using System.Threading.Tasks;

namespace MSD.Sales.Repository.Db.Repositories
{
    public class OrderRepositoryDb : CrudRepositoryBase<Order>, IOrderRepositoryDb
    {
        public OrderRepositoryDb(MainDbContext context) : base(context)
        {
        }
        
        public PagedResult<Domain.Dtos.Order> ListPage(int page = 1) => Page(Query(), page).To(e => new Domain.Dtos.Order(e));

        public async Task<Domain.Dtos.Order> GetByNumberAsync(string number) 
        {
            var entity = await FirstOrDefaultAsync(e => e.Number == number);

            if (entity == null)
                return null;

            return new Domain.Dtos.Order(entity);
        } 
    }
}
