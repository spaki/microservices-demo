using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using MSD.Product.Infra.Api;
using System.Net.Http;

namespace MSD.Product.Repository.API.Common
{
    public abstract class RepositoryApiBase : ApiClient, IRepositoryBase
    {
        protected RepositoryApiBase(
            HttpClient client, 
            ILogger<RepositoryApiBase> log
        ) : base(client, log)
        {
        }
    }
}
