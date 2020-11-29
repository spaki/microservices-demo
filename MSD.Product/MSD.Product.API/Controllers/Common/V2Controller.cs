using Microsoft.AspNetCore.Mvc;
using MSD.Product.Infra.Warning;

namespace MSD.Product.API.Controllers.Common
{
    [ApiVersion("2")]
    public abstract class V2Controller : RootController
    {
        public V2Controller(WarningManagement warningManagement) : base (warningManagement)
        {
        }
    }
}
