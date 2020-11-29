using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using MSD.Product.Infra;
using MSD.Product.Infra.Api;
using System.Net.Http;

namespace MSD.Product.Repository.ZipCode.V1.API.Common
{
    public abstract class ZipCodeRepositoryApiV1Base : ApiClient, IRepositoryBase
    {
        protected ZipCodeRepositoryApiV1Base(
            HttpClient client,
            ILogger<ZipCodeRepositoryApiV1Base> log
        ) : base(client, log)
        {
        }
    }
}
