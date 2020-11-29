using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using MSD.Product.Infra.Api;
using System.Net.Http;

namespace MSD.Product.Repository.ZipCode.V2.API.Common
{
    public abstract class ZipCodeRepositoryApiV2Base : ApiClient, IRepositoryBase
    {
        protected ZipCodeRepositoryApiV2Base(
            HttpClient client,
            ILogger<ZipCodeRepositoryApiV2Base> log
        ) : base(client, log)
        {
        }
    }
}
