using MSD.Product.Domain.Models.Common;
using System.Collections.Generic;

namespace MSD.Product.API.Models
{
    public class ApiDefaultResponseBase
    {
        public ApiDefaultResponseBase(bool success, List<Warning> messages)
        {
            Success = success;
            Messages = messages;
        }

        public bool Success { get; private set; }
        public List<Warning> Messages { get; private set; }
    }
}
