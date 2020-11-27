using MSD.Product.Domain.Enums;
using MSD.Product.Domain.Interfaces.Services;
using MSD.Product.Domain.Models.Common;
using MSD.Product.Domain.Services.Common;
using System.Collections.Generic;
using System.Linq;

namespace MSD.Product.Domain.Services
{
    public class WarningService : ServiceBase, IWarningService
    {
        private List<Warning> items = new List<Warning>();

        public void Add(Warning warning)
        {
            if (warning == null)
                return;

            items.Add(warning);
        }

        public void Add(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            items.Add(new Warning(message));
        }

        public void Add(string message, WarningType warningType)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            items.Add(new Warning(message, warningType));
        }

        public bool Any() => items.Any();

        public List<Warning> List() => items;
    }
}
