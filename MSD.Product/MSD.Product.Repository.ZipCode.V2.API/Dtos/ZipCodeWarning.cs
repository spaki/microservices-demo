using MSD.Product.Repository.ZipCode.V2.API.Enums;
using System;

namespace MSD.Product.Repository.ZipCode.V2.API.Dtos
{
    public class ZipCodeWarning
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public DateTime DateTimeUtc { get; private set; }
        public ZipCodeWarningType WarningType { get; private set; }
    }
}
