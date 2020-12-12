using System;

namespace MSD.Sales.Infra.NotificationSystem
{
    public class Notification
    {
        public Notification(string message) : this(message, NotificationType.Generic)
        { }

        public Notification(Exception exception) : this(exception.ToString(), NotificationType.Error)
        { }

        public Notification(string message, NotificationType type)
        {
            Message = message;
            DateTimeUtc = DateTime.UtcNow;
            Id = Guid.NewGuid();
            Type = type;
        }

        public Notification(Exception exception, NotificationType type) : this(exception.ToString(), type)
        { }

        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public DateTime DateTimeUtc { get; private set; }
        public NotificationType Type { get; private set; }
    }
}
