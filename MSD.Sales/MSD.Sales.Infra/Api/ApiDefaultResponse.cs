using MSD.Sales.Infra.NotificationSystem;
using System;
using System.Collections.Generic;

namespace MSD.Sales.Infra.Api
{
    public class ApiDefaultResponse<T> : ApiDefaultResponseBase
    {
        public ApiDefaultResponse()
        {

        }

        public ApiDefaultResponse(T payload) : this(payload, true, null) { } 

        public ApiDefaultResponse(T payload, bool success, List<Notification> notifications) : base(success, notifications) => Payload = payload;

        public T Payload { get; set; }

        public ApiDefaultResponse<TNewResult> To<TNewResult>(Func<T, TNewResult> conversion) => Payload != null ? new ApiDefaultResponse<TNewResult>(conversion.Invoke(Payload), Success, Notifications) : new ApiDefaultResponse<TNewResult>(default, Success, Notifications);
    }
}
