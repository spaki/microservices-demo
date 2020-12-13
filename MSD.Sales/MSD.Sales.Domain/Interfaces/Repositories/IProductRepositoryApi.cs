using MSD.Sales.Domain.Interfaces.Repositories.Common;
using MSD.Sales.Infra.Api;
using System.Threading.Tasks;

namespace MSD.Sales.Domain.Interfaces.Repositories
{
    public interface IProductRepositoryApi : IRepositoryBase
    {
        Task<ApiDefaultResponse<Dtos.Product>> GetByExternalIdAsync(string externalId);
    }
}
