using System.Collections.Generic;
using System.Linq;

namespace MSD.Sales.Infra.NotificationSystem
{
    public class NotificationManagement
    {
        private List<Notification> items = new List<Notification>();

        public void Add(Notification notification)
        {
            if (notification == null)
                return;

            items.Add(notification);
        }

        public void Add(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            items.Add(new Notification(message));
        }

        public void Add(string message, NotificationType type)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            items.Add(new Notification(message, type));
        }

        public bool Any() => items.Any();

        public void Clear() => items.Clear();

        public List<Notification> List() => items;
    }
}
