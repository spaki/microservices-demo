using MSD.Product.Infra.Warning.Dtos;
using MSD.Product.Infra.Warning.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MSD.Product.Infra.Warning
{
    public class WarningManagement
    {
        private List<WarningInfo> items = new List<WarningInfo>();

        public void Add(WarningInfo warning)
        {
            if (warning == null)
                return;

            items.Add(warning);
        }

        public void Add(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            items.Add(new WarningInfo(message));
        }

        public void Add(string message, WarningType warningType)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            items.Add(new WarningInfo(message, warningType));
        }

        public bool Any() => items.Any();

        public void Clear() => items.Clear();

        public List<WarningInfo> List() => items;
    }
}
