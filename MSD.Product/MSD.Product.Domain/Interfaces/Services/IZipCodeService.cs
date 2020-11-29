using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Services.Common;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Interfaces.Services
{
    public interface IZipCodeService : IServiceBase
    {
        Task<Address> GetAddressByZipCodeV1Async(string zipCode);
        Task<Address> GetAddressByZipCodeV2Async(string zipCode);
    }
}
