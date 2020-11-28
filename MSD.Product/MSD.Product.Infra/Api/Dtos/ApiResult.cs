using MSD.Product.Infra.Warning.Dtos;
using System;

namespace MSD.Product.Infra.Api.Dtos
{
    public class ApiResult<TResult> : ApiResultBase
    {
        public ApiResult(TResult result, string url) : base(url) => Result = result;

        public ApiResult(string url, WarningInfo warning) : base(url, warning)
        { }

        public ApiResult(string url, Exception exception) : base(url, exception)
        { }

        public ApiResult(TResult result, string url, WarningInfo warning) : base(url, warning) => Result = result;

        public TResult Result { get; private set; }

        public ApiResult<TNewResult> To<TNewResult>(Func<TResult, TNewResult> conversion) => Result != null ? new ApiResult<TNewResult>(conversion.Invoke(Result), Url, Warning) : new ApiResult<TNewResult>(Url, Warning);
    }
}
