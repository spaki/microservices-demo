using Microsoft.AspNetCore.WebUtilities;
using MSD.Product.Domain.Infra.Settings;
using MSD.Product.Domain.Interfaces.Repositories.Common;
using System.Collections.Generic;
using System.Linq;

namespace MSD.Product.Repository.API.Common
{
    public abstract class RepositoryApiBase : IRepositoryBase
    {
        protected readonly AppSettings settings;

        public RepositoryApiBase(AppSettings settings)
        {
            this.settings = settings;
        }

        public string BuildUrl(params KeyValuePair<string, object>[] parameters)
        {
            var dictionary = parameters
                .Where(e => e.Value != null && e.Key != null)
                .ToDictionary(e => e.Key, e => e.Value.ToString());

            var url = QueryHelpers.AddQueryString(settings.StarWarsApiUrl, dictionary);

            return url;
        }
    }
}
