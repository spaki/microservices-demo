using Microsoft.Extensions.Logging;
using MSD.Sales.Domain.Interfaces.Repositories.Common;
using MSD.Sales.Infra.Api;
using System.Net.Http;

namespace MSD.Sales.Repository.Product.Api.Common
{
    public abstract class ProductRepositoryApiBase : ApiClient, IRepositoryBase
    {
        protected ProductRepositoryApiBase(
            HttpClient client, 
            ILogger<ApiClient> log
        ) : base(client, log)
        {
        }
    }
}
