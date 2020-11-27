using MSD.ZipCode.V1.API.Model;

namespace MSD.ZipCode.V1.API.Dto
{
    public class Address
    {
        public Address(consultaCEPResponse model)
        {
            City = model.@return.cidade;
            State = model.@return.uf;
            Street = model.@return.end;
            AdditionalInfo = model.@return.complemento2;

            Neighborhood = model.@return.bairro;
            ZipCode = model.@return.cep;

            Formatted = $"{City}/{State} - {Street} - ";

            if (!string.IsNullOrWhiteSpace(AdditionalInfo))
                Formatted += AdditionalInfo + " - ";

            Formatted += $"{Neighborhood} - {ZipCode}";
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
