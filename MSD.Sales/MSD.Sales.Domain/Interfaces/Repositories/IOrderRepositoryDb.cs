using MSD.Sales.Domain.Dtos.Common;
using MSD.Sales.Domain.Interfaces.Repositories.Common;
using MSD.Sales.Domain.Models;
using System.Threading.Tasks;

namespace MSD.Sales.Domain.Interfaces.Repositories
{
    public interface IOrderRepositoryDb : ICrudRepositoryBase<Order>
    {
        PagedResult<Dtos.Order> ListPage(int page = 1);
        Task<Dtos.Order> GetByNumberAsync(string number);
    }
}
