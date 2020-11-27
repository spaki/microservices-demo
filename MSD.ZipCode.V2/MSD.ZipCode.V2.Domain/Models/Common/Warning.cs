using MSD.ZipCode.V2.Domain.Enums;
using MSD.ZipCode.V2.Domain.Models.Common;
using System;

namespace MSD.ZipCode.Domain.Models.Common
{
    public class Warning : EntityBase
    {
        public Warning(string message) : this(message, WarningType.Generic)
        { }

        public Warning(Exception exception) : this(exception.ToString(), WarningType.Generic)
        { }

        public Warning(string message, WarningType warningType)
        {
            Message = message;
            DateTimeUtc = DateTime.UtcNow;
            Id = Guid.NewGuid();
            WarningType = warningType;
        }

        public Warning(Exception exception, WarningType warningType) : this(exception.ToString(), warningType)
        { }

        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public DateTime DateTimeUtc { get; private set; }
        public WarningType WarningType { get; private set; }
    }
}
