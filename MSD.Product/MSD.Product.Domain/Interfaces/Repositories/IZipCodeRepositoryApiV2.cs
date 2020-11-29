using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using MSD.Product.Infra.Api.Dtos;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Interfaces.Repositories
{
    public interface IZipCodeRepositoryApiV2 : IRepositoryBase
    {
        Task<ApiResult<Address>> GetAddressByZipCodeAsync(string zipCode);
    }
}
