using Microsoft.Extensions.Logging;
using MSD.ZipCode.V2.Domain.Interfaces.Repositories.Common;
using Polly;
using Polly.Retry;
using System;

namespace MSD.ZipCode.V2.Repository.SOAP.Common
{
    public abstract class RepositorySoapBase : IRepositoryBase
    {
        protected readonly ILogger<RepositorySoapBase> log;

        public RepositorySoapBase(
            ILogger<RepositorySoapBase> log
        )
        {
            this.log = log;
        }

        protected AsyncRetryPolicy GetRetryPolicy()
        {
            var attempts = 3;
            var interval = TimeSpan.FromSeconds(2);

            var retryPolicy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                        attempts,
                        retryCount => interval,
                        (message, retryCount) => log.LogError($"SOAP Problem ({retryCount} attempt): {message}")
                    );

            return retryPolicy;
        }
    }
}
