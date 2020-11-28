using MSD.Product.Infra.Warning.Dtos;
using System;

namespace MSD.Product.Infra.Api.Dtos
{
    public class ApiResultBase
    {
        public ApiResultBase(string url) => Url = url;

        public ApiResultBase(string url, WarningInfo warning) : this(url) => Warning = warning;

        public ApiResultBase(string url, Exception exception) : this(url, new WarningInfo(exception))
        { }

        public string Url { get; private set; }
        public WarningInfo Warning { get; private set; }
        public bool Success { get => Warning == null; }
    }
}
