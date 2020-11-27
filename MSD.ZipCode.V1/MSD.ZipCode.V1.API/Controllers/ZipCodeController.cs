using Microsoft.AspNetCore.Mvc;
using MSD.ZipCode.V1.API.Dto;
using MSD.ZipCode.V1.API.Interfaces;
using MSD.ZipCode.V1.API.Model;
using System.ServiceModel;
using System.Web;

namespace MSD.ZipCode.V1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZipCodeController : ControllerBase
    {
        private readonly AppSettings settings;

        public ZipCodeController(AppSettings settings) => this.settings = settings;

        [HttpGet("{zipCode}")]
        public Dto.Address Get(string zipCode)
        {
            zipCode = HttpUtility.UrlDecode(zipCode);

            using (var channel = new ChannelFactory<IAtendeCliente>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), new EndpointAddress(settings.CorreiosWS)))
            {
                var client = channel.CreateChannel();
                var data = client.consultaCEP(new consultaCEP(zipCode));
                ((ICommunicationObject)client).Close();
                channel.Close();
                var result = new Address(data);
                return result;
            }
        }
    }
}
