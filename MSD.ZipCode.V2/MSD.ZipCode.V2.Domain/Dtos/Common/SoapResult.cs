using MSD.ZipCode.Domain.Models.Common;
using System;

namespace MSD.ZipCode.V2.Domain.Dtos.Common
{
    public class SoapResult<TResult>
    {
        public SoapResult(string url) => Url = url;

        public SoapResult(string url, Warning warning) : this(url) => Warning = warning;

        public SoapResult(string url, Exception exception) : this(url, new Warning(exception))
        { }

        public SoapResult(TResult result, string url) : this(url) => Result = result;

        public SoapResult(TResult result, string url, Warning warning) : this(url, warning) => Result = result;



        public string Url { get; private set; }
        public Warning Warning { get; private set; }
        public bool Success { get => Warning == null; }
        public TResult Result { get; private set; }
    }
}
