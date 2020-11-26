using MSD.Product.Domain.Enums;
using System;

namespace MSD.Product.Domain.Models.Common
{
    public class Warning : EntityBase
    {
        public Warning(string message) : this(message, WarningType.Generic)
        { }

        public Warning(string message, WarningType warningType)
        {
            Message = message;
            DateTimeUtc = DateTime.UtcNow;
            Id = Guid.NewGuid();
            WarningType = warningType;
        }

        public string Message { get; private set; }
        public DateTime DateTimeUtc { get; private set; }
        public WarningType WarningType { get; private set; }
    }
}
