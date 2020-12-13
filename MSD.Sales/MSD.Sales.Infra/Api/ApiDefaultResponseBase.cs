using MSD.Sales.Infra.NotificationSystem;
using System.Collections.Generic;

namespace MSD.Sales.Infra.Api
{
    public class ApiDefaultResponseBase
    {
        public ApiDefaultResponseBase()
        {

        }

        public ApiDefaultResponseBase(bool success, List<Notification> notifications)
        {
            Success = success;
            Notifications = notifications;
        }

        public bool Success { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
