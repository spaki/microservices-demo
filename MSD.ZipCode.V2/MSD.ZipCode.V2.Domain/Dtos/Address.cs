namespace MSD.ZipCode.V2.Domain.Dtos
{
    public class Address
    {
        public Address(ZipCode.Domain.Models.Address entity)
        {
            Neighborhood = entity.Neighborhood;
            City = entity.City;
            State = entity.State;
            Street = entity.Street;
            AdditionalInfo = entity.AdditionalInfo;
            ZipCode = entity.ZipCode;
            Formatted = entity.Formatted;
        }

        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Street { get; private set; }
        public string AdditionalInfo { get; private set; }
        public string ZipCode { get; private set; }
        public string Formatted { get; private set; }
    }
}
