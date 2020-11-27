namespace MSD.ZipCode.V1.API.Dto
{
    public class ApiDefaultResponse<T>
    {
        public ApiDefaultResponse(T payload, bool success, string message)
        {
            Payload = payload;
            Success = success;
            Message = message;
        }

        public T Payload { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }
    }
}
