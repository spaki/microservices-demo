using MSD.ZipCode.Domain.Models.Common;
using MSD.ZipCode.V2.Domain.Enums;
using MSD.ZipCode.V2.Domain.Interfaces.Services.Common;
using System.Collections.Generic;

namespace MSD.ZipCode.V2.Domain.Interfaces.Services
{
    public interface IWarningService : IServiceBase
    {
        void Add(Warning warning);
        void Add(string message);
        void Add(string message, WarningType warningType);
        bool Any();
        List<Warning> List();
    }
}
