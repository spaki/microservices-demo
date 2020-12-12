using MSD.Sales.Infra.NotificationSystem;
using System.Collections.Generic;

namespace MSD.Sales.Infra.Api
{
    public class ApiDefaultResponse<T> : ApiDefaultResponseBase
    {
        public ApiDefaultResponse(T payload, bool success, List<Notification> notifications) : base(success, notifications) => Payload = payload;

        public T Payload { get; private set; }
    }
}
