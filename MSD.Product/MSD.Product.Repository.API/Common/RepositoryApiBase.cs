using Microsoft.Extensions.Logging;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using MSD.Product.Infra;
using MSD.Product.Infra.Api;
using System.Net.Http;

namespace MSD.Product.Repository.API.Common
{
    public abstract class RepositoryApiBase : ApiClient, IRepositoryBase
    {
        protected RepositoryApiBase(
            HttpClient client, 
            AppSettings settings, 
            ILogger<RepositoryApiBase> log
        ) : base(client, settings, log)
        {
        }
    }
}
