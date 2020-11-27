namespace MSD.ZipCode.V1.API.Dto
{
    public class Address
    {
        public Address(Model.Address model)
        {
            City = model.Cidade;
            State = model.UF;
            Street = model.End;
            AdditionalInfo = model.Complemento2;

            Neighborhood = model.Bairro;
            ZipCode = model.Cep;

            Formatted = $"{City}/{State} - {Street} - ";

            if (string.IsNullOrWhiteSpace(AdditionalInfo))
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
