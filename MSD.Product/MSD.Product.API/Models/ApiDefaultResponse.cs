using MSD.Product.Domain.Models.Common;
using System.Collections.Generic;

namespace MSD.Product.API.Models
{
    public class ApiDefaultResponse<T> : ApiDefaultResponseBase
    {
        public ApiDefaultResponse(T payload, bool success, List<Warning> messages) : base (success, messages)
        {
            Payload = payload;
        }

        public T Payload { get; private set; }
    }
}
