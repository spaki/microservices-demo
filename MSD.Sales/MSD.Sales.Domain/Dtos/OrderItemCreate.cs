namespace MSD.Sales.Domain.Dtos
{
    public class OrderItemCreate
    {
        public string ExternalId { get; set; }
        public int Quantity { get; set; }
    }
}
