using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Dtos.ZipCode;
using MSD.Product.Domain.Interfaces.Repositories;
using MSD.Product.Infra;
using MSD.Product.Infra.Api.Dtos;
using MSD.Product.Infra.Warning;
using MSD.Product.Repository.ZipCode.V2.API.Common;
using MSD.Product.Repository.ZipCode.V2.API.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSD.Product.Repository.ZipCode.V2.API
{
    public class ZipCodeRepositoryApiV2 : ZipCodeRepositoryApiV2Base, IZipCodeRepositoryApiV2
    {
        private readonly AppSettings settings;
        private WarningManagement warningManagement;

        public ZipCodeRepositoryApiV2(
            HttpClient client,
            AppSettings settings,
            WarningManagement warningManagement,
            ILogger<ZipCodeRepositoryApiV2> log
        ) : base(client, log)
        {
            this.settings = settings;
            this.warningManagement = warningManagement;
        }

        public async Task<ApiResult<Address>> GetAddressByZipCodeAsync(string zipCode)
        {
            var url = BuildUrl(settings.ZipCodeApiV2, zipCode, true);
            log.LogInformation($"accessing {url}");

            var apiResult = await GetAsync<ZipCodeResponse<Address>>(url);
            apiResult.Result?.Messages?.ForEach(item => warningManagement.Add(item.Message));
            
            var result = apiResult.To(e => e.Payload);

            return result;
        }
    }
}
