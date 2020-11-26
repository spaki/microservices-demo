namespace MSD.Product.Domain.Dtos.Common
{
    public class ApiResultBase
    {
        public ApiResultBase(string url)
        {
            Url = url;
        }

        public ApiResultBase(string url, bool success, string message = null)
        {
            Url = url;
            Success = success;
            Message = message;
        }

        public string Url { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }
    }
}
