using System;

namespace MSD.Product.Domain.Dtos.Common
{
    public class ApiResult<TResult> : ApiResultBase
    {
        public ApiResult(TResult result, string url) : base(url) => Result = result;

        public ApiResult(TResult result, string url, bool success, string message = null) : base(url, success, message) => Result = result;

        public TResult Result { get; private set; }

        public ApiResult<TNewResult> To<TNewResult>(Func<TResult, TNewResult> conversion) => new ApiResult<TNewResult>(conversion.Invoke(Result), Url, Success, Message);
    }
}
