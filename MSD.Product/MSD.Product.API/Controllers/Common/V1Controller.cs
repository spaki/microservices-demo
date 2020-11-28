using Microsoft.AspNetCore.Mvc;
using MSD.Product.Infra.Warning;

namespace MSD.Product.API.Controllers.Common
{
    [ApiVersion("1")]
    public class V1Controller : RootController
    {
        public V1Controller(WarningManagement warningManagement) : base (warningManagement)
        {
        }
    }
}
