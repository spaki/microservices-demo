using MSD.Product.Domain.Models.Common;
using System.Collections.Generic;

namespace MSD.Product.API.Models
{
    public class ApiDefaultResponse
    {
        public ApiDefaultResponse(object payload, bool success, List<Warning> messages)
        {
            Payload = payload;
            Success = success;
            Messages = messages;
        }

        public object Payload { get; private set; }
        public bool Success { get; private set; }
        public List<Warning> Messages { get; private set; }
    }
}
