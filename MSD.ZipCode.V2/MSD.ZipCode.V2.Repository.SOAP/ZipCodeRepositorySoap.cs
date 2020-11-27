using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MSD.ZipCode.Domain.Models;
using MSD.ZipCode.V2.Domain.Dtos.Common;
using MSD.ZipCode.V2.Domain.Infra;
using MSD.ZipCode.V2.Domain.Infra.Settings;
using MSD.ZipCode.V2.Domain.Interfaces.Repositories;
using MSD.ZipCode.V2.Repository.SOAP.Common;
using MSD.ZipCode.V2.Repository.SOAP.Dto;
using MSD.ZipCode.V2.Repository.SOAP.Interfaces;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MSD.ZipCode.V2.Repository.SOAP
{
    public class ZipCodeRepositorySoap : RepositorySoapBase, IZipCodeRepositorySoap
    {
        private readonly AppSettings settings;
        private readonly IDistributedCache cache;

        public ZipCodeRepositorySoap(
            AppSettings settings,
            IDistributedCache cache,
            ILogger<ZipCodeRepositorySoap> log
        ) : base(cache, log)
        {
            this.settings = settings;
        }

        public async Task<SoapResult<Address>> GetAddressAsync(string zipCode) => await GetFromCacheAndSetAsync(Constants.ZipCodeCacheResultKey, zipCode, async () => await GetAddressFromSoapAsync(zipCode));

        private async Task<SoapResult<Address>> GetAddressFromSoapAsync(string zipCode)
        {
            var retryPolice = GetRetryPolicy();
            var executionResult = await retryPolice.ExecuteAndCaptureAsync(async () =>
            {
                using (var channel = new ChannelFactory<IAtendeCliente>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), new EndpointAddress(settings.CorreiosWS)))
                {
                    var client = channel.CreateChannel();
                    var data = client.consultaCEP(new consultaCEP(zipCode));
                    ((ICommunicationObject)client).Close();
                    channel.Close();
                    var entity = new Address(data.@return.bairro, data.@return.cidade, data.@return.uf, data.@return.end, data.@return.complemento2, data.@return.cep);
                    var result = new SoapResult<Address>(entity, settings.CorreiosWS);
                    return result;
                }
            });

            if (executionResult.FinalException != null)
                return new SoapResult<Address>(settings.CorreiosWS, executionResult.FinalException);

            return executionResult.Result;
        }
    }
}
