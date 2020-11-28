namespace MSD.ZipCode.V2.Domain.Infra
{
    public class Constants
    {
        public const string ZipCodeCacheResultKey = "ZipCodeCacheResultKey";
        public const int CacheTimeoutInSeconds = 20;
        public const int CiscuitBreakerAttempts = 3;
        public const int CiscuitBreakerIntervalInSeconds = 2;
    }
}
