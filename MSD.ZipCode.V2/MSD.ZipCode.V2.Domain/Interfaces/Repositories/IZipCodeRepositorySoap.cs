using MSD.ZipCode.Domain.Models;
using MSD.ZipCode.V2.Domain.Dtos.Common;
using System.Threading.Tasks;

namespace MSD.ZipCode.V2.Domain.Interfaces.Repositories
{
    public interface IZipCodeRepositorySoap
    {
        Task<SoapResult<Address>> GetAddressAsync(string zipCode);
    }
}
