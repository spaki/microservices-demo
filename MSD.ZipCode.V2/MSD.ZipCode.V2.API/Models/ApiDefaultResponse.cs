using MSD.ZipCode.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSD.ZipCode.V2.API.Models
{
    public class ApiDefaultResponse<T>
    {
        public ApiDefaultResponse(bool success, List<Warning> messages)
        {
            Success = success;
            Messages = messages;
        }

        public ApiDefaultResponse(T payload, bool success, List<Warning> messages) : this(success, messages) => Payload = payload;

        public bool Success { get; private set; }
        public List<Warning> Messages { get; private set; }
        public T Payload { get; private set; }
    }
}
