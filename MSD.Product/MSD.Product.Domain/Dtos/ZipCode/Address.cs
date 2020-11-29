namespace MSD.Product.Domain.Dtos.ZipCode
{
    public class Address
    {
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string AdditionalInfo { get; set; }
        public string ZipCode { get; set; }
        public string Formatted { get; set; }
    }
}
