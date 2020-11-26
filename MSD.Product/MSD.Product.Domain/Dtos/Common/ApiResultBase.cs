using MSD.Product.Domain.Models.Common;
using System;

namespace MSD.Product.Domain.Dtos.Common
{
    public class ApiResultBase
    {
        public ApiResultBase(string url)
        {
            Url = url;
        }

        public ApiResultBase(string url, Warning warning) : this(url)
        {
            Warning = warning;
        }

        public ApiResultBase(string url, Exception exception) : this(url, new Warning(exception)) 
        { }

        public string Url { get; private set; }
        public Warning Warning { get; private set; }
        public bool Success { get => Warning == null; }
    }
}
