using MSD.ZipCode.V2.Domain.Interfaces.Services.Common;
using System.Threading.Tasks;

namespace MSD.ZipCode.V2.Domain.Interfaces.Services
{
    public interface IZipCodeService: IServiceBase
    {
        Task<Dtos.Address> GetAddressAsync(string zipCode);
    }
}
