using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace MSD.ZipCode.V1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZipCodeController : ControllerBase
    {
        [HttpGet("{zipCode}")]
        public Dto.Address Get(string zipCode)
        {
            zipCode = HttpUtility.UrlDecode(zipCode);
            var result = new Dto.Address(new Model.Address { Cep = zipCode });
            return result;
        }
    }
}
