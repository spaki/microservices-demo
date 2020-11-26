using MSD.Product.Domain.Dtos.Common;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Interfaces.Repositories
{
    public interface IProductRepositoryApi : IRepositoryBase
    {
        Task<ApiResult<PagedResult<Models.Product>>> SearchAsync(string value = null, int page = 1);
    }
}
