using MSD.Product.Infra.Warning.Dtos;
using System.Collections.Generic;

namespace MSD.Product.API.Models
{
    public class ApiDefaultResponseBase
    {
        public ApiDefaultResponseBase(bool success, List<WarningInfo> messages)
        {
            Success = success;
            Messages = messages;
        }

        public bool Success { get; private set; }
        public List<WarningInfo> Messages { get; private set; }
    }
}
