namespace MSD.Product.Repository.ZipCode.V1.API.Dtos
{
    public class ZipCodeResponse<T>
    {
        public T Payload { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
