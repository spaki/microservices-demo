using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSD.ZipCode.V1.API.Dto;
using MSD.ZipCode.V1.API.Interfaces;
using MSD.ZipCode.V1.API.Model;
using System;
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
        public ActionResult<ApiDefaultResponse<Address>> Get(string zipCode)
        {
            try
            {
                zipCode = HttpUtility.UrlDecode(zipCode);

                using (var channel = new ChannelFactory<IAtendeCliente>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), new EndpointAddress(settings.CorreiosWS)))
                {
                    var client = channel.CreateChannel();
                    var data = client.consultaCEP(new consultaCEP(zipCode));
                    ((ICommunicationObject)client).Close();
                    channel.Close();
                    var entity = new Address(data);

                    return Ok(new ApiDefaultResponse<Address>(entity, true, null));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiDefaultResponse<Address>(null, false, ex.ToString()));
            }
        }
    }
}
