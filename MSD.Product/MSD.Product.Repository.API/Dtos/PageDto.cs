using System.Collections.Generic;

namespace MSD.Product.Repository.API.Dtos
{
    internal class PageDto<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<T> results { get; set; }
    }
}
