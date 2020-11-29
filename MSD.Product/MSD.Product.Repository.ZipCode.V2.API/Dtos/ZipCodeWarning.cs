using MSD.Product.Repository.ZipCode.V2.API.Enums;
using System;

namespace MSD.Product.Repository.ZipCode.V2.API.Dtos
{
    public class ZipCodeWarning
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public ZipCodeWarningType WarningType { get; set; }
    }
}
