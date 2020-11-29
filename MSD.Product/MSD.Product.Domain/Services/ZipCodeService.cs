using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Domain.Interfaces.Services;
using MSD.Product.Domain.Services.Common;
using MSD.Product.Infra.Warning;
using System.Threading.Tasks;

namespace MSD.Product.Domain.Services
{
    public class ZipCodeService : ServiceBase, IZipCodeService
    {
        private readonly WarningManagement warningManagement;
        private readonly IZipCodeRepositoryApiV1 zipCodeRepositoryApiV1;

        public ZipCodeService(
            WarningManagement warningManagement,
            IZipCodeRepositoryApiV1 zipCodeRepositoryApiV1
        )
        {
            this.warningManagement = warningManagement;
            this.zipCodeRepositoryApiV1 = zipCodeRepositoryApiV1;
        }

        public async Task<Address> GetAddressByZipCodeV1Async(string zipCode)
        {
            var apiResult = await zipCodeRepositoryApiV1.GetAddressByZipCodeAsync(zipCode);

            warningManagement.Add(apiResult.Warning);

            return apiResult.Result;
        }
    }
}
