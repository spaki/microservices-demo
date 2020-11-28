using MSD.Product.Infra.Warning.Enums;
using System;

namespace MSD.Product.Infra.Warning.Dtos
{
    public class WarningInfo
    {
        public WarningInfo(string message) : this(message, WarningType.Generic)
        { }

        public WarningInfo(Exception exception) : this(exception.ToString(), WarningType.Generic)
        { }

        public WarningInfo(string message, WarningType warningType)
        {
            Message = message;
            DateTimeUtc = DateTime.UtcNow;
            Id = Guid.NewGuid();
            WarningType = warningType;
        }

        public WarningInfo(Exception exception, WarningType warningType) : this(exception.ToString(), warningType)
        { }

        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public DateTime DateTimeUtc { get; private set; }
        public WarningType WarningType { get; private set; }
    }
    
}
