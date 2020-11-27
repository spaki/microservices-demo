using MSD.ZipCode.V2.Domain.Models.Common;

namespace MSD.ZipCode.Domain.Models
{
    public class Address : EntityBase
    {
        public Address()
        {

        }

        public Address(string neighborhood, string city, string state, string street, string additionalInfo, string zipCode)
        {
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Street = street;
            AdditionalInfo = additionalInfo;
            ZipCode = zipCode;
        }

        public virtual string Neighborhood { get; private set; }
        public virtual string City { get; private set; }
        public virtual string State { get; private set; }
        public virtual string Street { get; private set; }
        public virtual string AdditionalInfo { get; private set; }
        public virtual string ZipCode { get; private set; }



        private string formatted;

        public string Formatted 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(formatted))
                {
                    formatted = $"{City}/{State} - {Street} - ";

                    if (!string.IsNullOrWhiteSpace(AdditionalInfo))
                        formatted += AdditionalInfo + " - ";

                    formatted += $"{Neighborhood} - {ZipCode}";
                }

                return formatted;
            }
        }
    }
}
