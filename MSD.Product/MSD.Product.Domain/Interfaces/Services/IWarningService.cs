using MSD.Product.Domain.Enums;
using MSD.Product.Domain.Interfaces.Services.Common;
using MSD.Product.Domain.Models.Common;
using System.Collections.Generic;

namespace MSD.Product.Domain.Interfaces.Services
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
