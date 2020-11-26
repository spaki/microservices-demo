using MSD.Product.Domain.Models.Common;
using System;

namespace MSD.Product.Domain.Dtos.Common
{
    public class ApiResult<TResult> : ApiResultBase
    {
        public ApiResult(TResult result, string url) : base(url) => Result = result;

        public ApiResult(string url, Warning warning) : base(url, warning) 
        { }

        public ApiResult(string url, Exception exception) : base(url, exception)
        { }

        public ApiResult(TResult result, string url, Warning warning) : base(url, warning) => Result = result;

        public TResult Result { get; private set; }

        public ApiResult<TNewResult> To<TNewResult>(Func<TResult, TNewResult> conversion) => new ApiResult<TNewResult>(conversion.Invoke(Result), Url, Warning);
        public ApiResult<TNewResult> ToEmptyResultOf<TNewResult>() => new ApiResult<TNewResult>(Url, Warning);
    }
}
