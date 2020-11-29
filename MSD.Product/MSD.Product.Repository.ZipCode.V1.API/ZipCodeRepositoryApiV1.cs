using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Infra;
using MSD.Product.Infra.Api.Dtos;
using MSD.Product.Infra.Warning;
using MSD.Product.Repository.ZipCode.V1.API.Common;
using MSD.Product.Repository.ZipCode.V1.API.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSD.Product.Repository.ZipCode.V1.API
{
    public class ZipCodeRepositoryApiV1 : ZipCodeRepositoryApiV1Base, IZipCodeRepositoryApiV1
    {
        private readonly AppSettings settings;
        private WarningManagement warningManagement;

        public ZipCodeRepositoryApiV1(
            HttpClient client, 
            AppSettings settings,
            WarningManagement warningManagement,
            ILogger<ZipCodeRepositoryApiV1> log
        ) : base(client, log)
        {
            this.settings = settings;
            this.warningManagement = warningManagement;
        }

        public async Task<ApiResult<Address>> GetAddressByZipCodeAsync(string zipCode)
        {
            var apiResult = await GetAsync<ZipCodeResponse<Address>>(BuildUrl(settings.ZipCodeApiV1, zipCode, true));
            var result = apiResult.To(e => e.Payload);

            if (!apiResult.Success && !string.IsNullOrWhiteSpace(apiResult.Result.Message))
                warningManagement.Add(apiResult.Result.Message);

            return result;
        }
    }
}
