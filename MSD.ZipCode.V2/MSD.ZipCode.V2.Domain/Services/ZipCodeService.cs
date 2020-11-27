using MSD.ZipCode.V2.Domain.Interfaces.Repositories;
using MSD.ZipCode.V2.Domain.Interfaces.Services;
using MSD.ZipCode.V2.Domain.Services.Common;
using System.Threading.Tasks;

namespace MSD.ZipCode.V2.Domain.Services
{
    public class ZipCodeService : ServiceBase, IZipCodeService
    {
        private readonly IWarningService warningService;
        private readonly IZipCodeRepositorySoap zipCodeRepositorySoap;

        public ZipCodeService(
            IWarningService warningService,
            IZipCodeRepositorySoap zipCodeRepositorySoap
        )
        {
            this.warningService = warningService;
            this.zipCodeRepositorySoap = zipCodeRepositorySoap;
        }

        public async Task<Dtos.Address> GetAddressAsync(string zipCode) 
        {
            var soapResult = await zipCodeRepositorySoap.GetAddressAsync(zipCode);

            warningService.Add(soapResult.Warning);

            var result = soapResult.Success ? new Dtos.Address(soapResult.Result) : null; 

            return result;
        }
    }
}
