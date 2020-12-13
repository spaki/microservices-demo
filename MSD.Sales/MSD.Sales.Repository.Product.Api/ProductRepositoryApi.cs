using Microsoft.Extensions.Logging;
using MSD.Sales.Domain.Interfaces.Repositories;
using MSD.Sales.Infra.Api;
using MSD.Sales.Infra.Settings;
using MSD.Sales.Repository.Product.Api.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSD.Sales.Repository.Product.Api
{
    public class ProductRepositoryApi : ProductRepositoryApiBase, IProductRepositoryApi
    {
        private readonly AppSettings settings;

        public ProductRepositoryApi(
            HttpClient client, 
            ILogger<ApiClient> log,
            AppSettings settings
        ) : base(client, log)
        {
            this.settings = settings;
        }

        public async Task<ApiDefaultResponse<Domain.Dtos.Product>> GetByExternalIdAsync(string externalId)
        {
            var response = await GetAsync<ApiDefaultResponse<Domain.Dtos.Product>>(BuildUrl(settings.ProductApi, "Product", externalId, true));
            var result = response.To(e => response?.Payload?.Payload);
            return result;
        }
    }
}
