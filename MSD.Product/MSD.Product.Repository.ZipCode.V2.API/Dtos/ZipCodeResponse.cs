using System.Collections.Generic;

namespace MSD.Product.Repository.ZipCode.V2.API.Dtos
{
    public class ZipCodeResponse<T>
    {
        public bool Success { get; set; }
        public List<ZipCodeWarning> Messages { get; set; }
        public T Payload { get; set; }
    }
}
