using Microsoft.AspNetCore.Mvc;
using MSD.Product.Domain.Interfaces.Services;

namespace MSD.Product.API.Controllers.Common
{
    [ApiVersion("1")]
    public class V1Controller : RootController
    {
        public V1Controller(IWarningService warningService) : base (warningService)
        {
        }
    }
}
